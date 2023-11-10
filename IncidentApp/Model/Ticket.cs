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
        public int Id { get; private set; }
        public Severity Severity { get; private set; }
        public Status Status { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set; }
        public Incident Incident { get; private set; }

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

        public int getID() { return Id; }
        public Severity getSeverity() { return Severity; }
        public Status getStatus() { return Status; }
        public DateTime getDateCreated() { return DateCreated; }
        public DateTime getDateUpdated() { return DateUpdated; }
        public Incident getIncident() { return Incident; }

        public Ticket()
        {

        }
    }
}
