using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ticket
    {
        // Ticket - Ignas
        private int _id;
        public int Id 
        { 
            get {  return _id; } 
            set {  _id = value; }
        }

        private Severity _severity;
        public Severity Severity 
        {
            get { return _severity; } 
            set { _severity = value; }
        }

        private Status _status;
        public Status Status 
        {
            get { return _status; }
            set { _status = value; }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated 
        { 
            get { return  _dateCreated; } 
            set { _dateCreated = value; } 
        }

        private DateTime _dateUpdated;
        public DateTime DateUpdated 
        { 
            get { return _dateUpdated; } 
            set { _dateUpdated = value; }
        } 

        private Incident _incident;
        public Incident Incident 
        { 
            get { return _incident; } 
            set { _incident = value; }
        }

        public Ticket(int id, int severity, int status, DateTime dateCreated, DateTime dateUpdated, Incident incident)
        {
            Id = id;
            Severity = (Severity)severity;
            Status = (Status)status;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            Incident = incident;
        }
    }
}
