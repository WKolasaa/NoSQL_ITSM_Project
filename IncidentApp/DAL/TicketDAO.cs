using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TicketDAO : DAO
    {
        // TicketDao setup - Ignas
        IMongoCollection<BsonDocument> ticketCollection;

        public TicketDAO()
        {
            setTicketCollection();
        }

        private void setTicketCollection()
        {
            this.ticketCollection = getCollection("Tickets");
        }

        // Ticket creation - Ignas
        public void createNewTicket(Ticket ticket)
        {
            BsonDocument document = convertTicketToBsonDocument(ticket);
            ticketCollection.InsertOne(document);
        }

        private BsonDocument convertTicketToBsonDocument(Ticket ticket)
        {
            Incident incident = ticket.getIncident();
            var document = new BsonDocument {
                { "_id", ticket.getID() },
                { "severity", ticket.getSeverity() },
                { "status", ticket.getStatus() },
                { "dateCreated", ticket.getDateCreated().ToString("f") },
                { "dateUpdated", ticket.getDateUpdated().ToString("f") },
                { "incident", new BsonDocument {
                    {"_id", incident.getID() },
                    {"type", incident.getType() },
                    {"reporter", incident.getReporter() },
                    {"description", incident.getDesc() }
                    }
                }
            };
            return document;
        }

        public int getLastTicketID()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var sort = Builders<BsonDocument>.Sort.Descending("_id");
            var projection = Builders<BsonDocument>.Projection.Include("_id").Slice("_id", 1);

            var result = ticketCollection.Find(filter).Sort(sort).Project(projection).FirstOrDefault();

            if (result != null)
            {
                return result["_id"].ToInt32();
            }
            throw new Exception();
        }

        public int getTicketsCount()
        {
            int ticketCount = (int)ticketCollection.CountDocuments(new BsonDocument());
            return ticketCount;
        }

        public int getOpenedTicketCount()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("status", Status.Open);
            int openedTicketCount = (int)ticketCollection.CountDocuments(filter);
            return openedTicketCount;
        }

        public int getResolvedTicketCount()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("status", Status.Resolved);
            int resolvedTicketCount = (int)ticketCollection.CountDocuments(filter);
            return resolvedTicketCount;
        }

        public int getClosedTicketCount()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("status", Status.ForceClosed);
            int closedTicketCount = (int)ticketCollection.CountDocuments(filter);
            return closedTicketCount;
        }

        public List<Ticket> GetTicketsByEmployeeName(string employeeName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("reporter", employeeName);
            var result = ticketCollection.Find(filter).ToList();

            List<Ticket> tickets = new List<Ticket>();

            foreach (var bsonDocument in result)
            {
                tickets.Add(BsonSerializer.Deserialize<Ticket>(bsonDocument));
            }

            return tickets;
        }
    }
}
