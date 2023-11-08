using Model;
using MongoDB.Bson;
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
    }
}
