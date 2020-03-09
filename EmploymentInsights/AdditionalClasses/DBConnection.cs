using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmploymentInsights.Models;
using MongoDB.Bson;

namespace EmploymentInsights.AdditionalClasses
{
    public class DBConnection
    {
        MongoClient client;
        string dbName;
        string collectionName;

        public void Initialize() {
            client = new MongoClient("mongodb+srv://JacobKrmn:root@employmentinsights-edrz0.gcp.mongodb.net/test?retryWrites=true&w=majority");
            dbName = "EmploymentInsights";
            collectionName = "Vacatures";
        }

        public void UploadVacature(Vacature vacature) {
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            var document = new BsonDocument { { "id", vacature.id},
                {"title", vacature.title },
                {"description" , vacature.description } };
            collection.InsertOne(document);

        }

        public bool isNotExisting(int id)
        {
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            var filter = Builders<BsonDocument>.Filter.Eq("id", id);
            var vacatureDocument = collection.Find(filter).FirstOrDefault();

            return vacatureDocument == null;
        }
    }
}