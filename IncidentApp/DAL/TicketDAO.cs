using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            
            Incident incident = ticket.Incident;
            var document = new BsonDocument {
                { "_id", ticket.Id },
                { "severity", ticket.Severity },
                { "status", ticket.Status },
                { "dateCreated", ticket.DateCreated.ToString("f") },
                { "dateUpdated", ticket.DateUpdated.ToString("f") },
                { "incident", new BsonDocument {
                    {"_id", incident.Id },
                    {"type", incident.Type },
                    {"reporter", incident.Reporter },
                    {"description", incident.Description }
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

        // Get tickets - Ignas
        public List<Ticket> getTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = ticketCollection.Find(filter).ToList();

            foreach (var document in result)
            {
                tickets.Add(convertBsonDocumentToTicket(document));
            }
            return tickets;
        }

        public List<Ticket> getNotClosedTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            var filter = Builders<BsonDocument>.Filter.Ne("status", Status.ForceClosed);
            var result = ticketCollection.Find(filter).ToList();

            foreach (var document in result)
            {
                tickets.Add(convertBsonDocumentToTicket(document));
            }
            return tickets;
        }

        // Close ticket - Ignas
        public void closeTicket(Ticket ticket)
        {
            var filt = Builders<BsonDocument>.Filter.Eq("_id", ticket.Id);
            var upd = Builders<BsonDocument>.Update.Set("status", 1);

            var result = ticketCollection.UpdateOne(filt, upd);
        }

        // get amount of tickets - Rienat
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

        // Wojtek
        public List<Ticket> GetTicketsForUser(string reporter)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("incident.reporter", reporter);
            var bsonDocuments = ticketCollection.Find(filter).ToList();

            var matchingTickets = bsonDocuments.Select(doc => convertBsonDocumentToTicket(doc)).ToList();
            return matchingTickets;
        }

        private Ticket convertBsonDocumentToTicket(BsonDocument document)
        {
            DateTime dateCreate = DateTime.Parse(document["dateCreated"].ToString()); // changed from the original to better parsing for DateTime
            DateTime dateUpdate = DateTime.Parse(document["dateUpdated"].ToString());
            Incident incident = new Incident(
                document["incident"]["_id"].ToInt32(), 
                document["incident"]["type"].ToInt32(), 
                document["incident"]["reporter"].ToString(), 
                document["incident"]["description"].ToString());
            
            Ticket ticket = new Ticket(
                document["_id"].ToInt32(), 
                document["severity"].ToInt32(), 
                document["status"].ToInt32(), 
                dateCreate, 
                dateUpdate, 
                incident);
            return ticket;
        }

        public void removeTicket(int id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            ticketCollection.DeleteOne(filter);
        }

        public void updateTicket(Ticket ticket)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ticket.Id);
            var update = Builders<BsonDocument>.Update.Set("severity", ticket.Severity).Set("status", ticket.Status).Set("dateUpdated", ticket.DateUpdated);
            ticketCollection.UpdateOne(filter, update);
        }
    }
}
