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
        private int _id;
        public int Id 
        { 
            get { return _id; } 
            set { _id = value; }
        }

        private IncidentType _type;
        public IncidentType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _reporter; // swap for user when User class done
        public string Reporter
        {
            get { return _reporter; }
            set { _reporter = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Incident(int id, int type, string reporter, string description) {
            Id = id;
            Type = (IncidentType)type;
            Reporter = reporter;
            Description = description;
        }
    }
}
