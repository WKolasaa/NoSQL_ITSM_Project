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
        private int Id;
        private Severity Severity;
        private Status Status;
        private DateTime DateCreated;
        private DateTime DateUpdated;
        private Incident Incident;

        public Ticket(int id, int severity, int status, DateTime dateCreated, DateTime dateUpdated, Incident incident)
        {
            this.Id = id;
            this.Severity = (Severity)severity;
            this.Status = (Status)status;
            this.DateCreated = dateCreated;
            this.DateUpdated = dateUpdated;
            this.Incident = incident;
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
