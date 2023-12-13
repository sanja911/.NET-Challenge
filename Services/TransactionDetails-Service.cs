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

namespace Transaction.Detail.Services
{
    public class TransactionDetailService
    {
        private readonly IMongoCollection<TransactionDetail> transactionDetails;
        public TransactionDetailService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("ProjectDb"));
            var database = client.GetDatabase("ProjectDb");
            transactionDetails = database.GetCollection<TransactionDetail>("TransactionDetails");
        }

        public TransactionDetail Create(TransactionDetail transactionDetail)
        {
            transactionDetails.InsertOne(transactionDetail);
            return transactionDetail;
        }

        public TransactionDetail Update(string id, TransactionDetail transactionDetail)
        {
            FilterDefinition<TransactionDetail> filter = Builders<TransactionDetail>.Filter.Eq("Id", id);
            UpdateDefinition<TransactionDetail> update = Builders<TransactionDetail>.Update.Set(transaction => transaction, transactionDetail);
            transactionDetails.UpdateOne(filter, update);
            return transactionDetail;
        }

        public List<TransactionDetail> GetTransactionDetails() =>transactionDetails.Find(detail => true).ToList();
        public TransactionDetail GetTransactionDetail(string id) =>transactionDetails.Find<TransactionDetail>(detail => detail.Id == id).FirstOrDefault();
    }
}