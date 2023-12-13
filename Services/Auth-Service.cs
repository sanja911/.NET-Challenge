using System;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Projects.Models;
using Projects.Database;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Projects.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> users;
        private readonly string key;
        public AuthService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ProjectDb"));
            var database = client.GetDatabase("ProjectDb");
            users = database.GetCollection<User>("Users");
            this.key = configuration.GetSection("JwtKey").ToString();
        }

        public User Create(User user)
        {
            users.InsertOne(user);
            return user;
        }

        
        public List<User> GetUsers(string email) =>users.Find<User>(user => user.Email == email).ToList();
        public string Authenticate(string email, string password)
        {
            var user = this.users.Find(user => user.Email == email && user.Password == password).FirstOrDefault();
            if (user == null)
                return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}