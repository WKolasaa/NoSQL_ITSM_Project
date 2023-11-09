using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Model;
using System.Data.Common;



namespace DAL
{
    public class DAO
    {
        //Db connection - Ignas
        private const string dbConnection = "mongodb+srv://incidentAppAdmin:admin123@clustertest.8blrxea.mongodb.net/?retryWrites=true&w=majority";
        private const string dbName = "InhollandNoSQLProjectIncidentApp";

        private MongoClient client;
        protected IMongoDatabase database;

        public DAO()
        {
            this.client = new MongoClient(dbConnection);
            this.database = client.GetDatabase(dbName);
        }

        protected IMongoCollection<BsonDocument> getCollection(string selectedCollectionName)
        {
            return database.GetCollection<BsonDocument>(selectedCollectionName);
        }
    }
}