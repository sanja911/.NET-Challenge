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
using Microsoft.OpenApi.Any;

namespace Transaction.Services
{
    public class TransactionHeaderService
    {
        private readonly IMongoCollection<TransactionHeader> transactionHeaders;
        public TransactionHeaderService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ProjectDb"));
            var database = client.GetDatabase("ProjectDb");
            transactionHeaders = database.GetCollection<TransactionHeader>("TransactionHeaders");
        }

        public TransactionHeader Create(TransactionHeader transactionHeader)
        {
            transactionHeaders.InsertOne(transactionHeader);
            return transactionHeader;
        }

        public TransactionHeader Update(string id, TransactionHeader transactionHeader)
        {
            FilterDefinition<TransactionHeader> filter = Builders<TransactionHeader>.Filter.Eq("Id", id);
            UpdateDefinition<TransactionHeader> update = Builders<TransactionHeader>.Update.Set(transaction => transaction, transactionHeader);
            transactionHeaders.UpdateOne(filter, update);
            return transactionHeader;
        }

        public List<TransactionHeader> GetTransactionHeaders() => transactionHeaders.Find(header => true).ToList();

        public TransactionHeader GetTransactionHeader(string id) => transactionHeaders.Find(header => header.Id == id).FirstOrDefault();
    }
}