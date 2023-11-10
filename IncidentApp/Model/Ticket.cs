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
