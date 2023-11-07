using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Model;
using System.Data.Common;

namespace DAL
{
    public class DAO
    {
        const string dbConnection = "mongodb+srv://incidentAppAdmin:admin123@clustertest.8blrxea.mongodb.net/?retryWrites=true&w=majority";
        const string dbName = "InhollandNoSQLProjectIncidentApp";

        private MongoClient client;
        private IMongoDatabase database;

        public DAO()
        {
            setMongoClient();
            setDatabase();
        }

        private void setMongoClient()
        {
            client = new MongoClient(dbConnection);
        }

        private void setDatabase()
        {
            this.database = client.GetDatabase(dbName);
        }
        
        private IMongoCollection<BsonDocument> getCollection(string selectedCollectionName)
        {
            return database.GetCollection<BsonDocument>(selectedCollectionName);
        }

        public void getEmployees()
        {
            IMongoCollection<BsonDocument> collection = getCollection("Employees");
            var bsonDocs = collection.Find(new BsonDocument()).ToList();
            foreach (var document in bsonDocs)
            {
                Console.WriteLine(document.ToJson().ToString());
            }
        }
    }
}