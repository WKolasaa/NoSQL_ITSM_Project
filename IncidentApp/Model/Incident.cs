using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Incident
    {
        // Incident - Ignas
        private int Id;
        private IncidentType Type;
        private string Reporter; // swap for user when User class done
        private string Description;

        public Incident(int id, int type, string reporter, string description) {
            this.Id = id;
            this.Type = (IncidentType)type;
            this.Reporter = reporter;
            this.Description = description;
        }

        public int getID() { return Id; }
        public IncidentType getType() { return Type; }
        public string getReporter() { return Reporter;}
        public string getDesc() { return Description; }
    }
}
