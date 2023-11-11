using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IncidentManagementApplication
{
    internal class LoggedUser
    {
        private static LoggedUser uniqueInstance;
        private User loggedUser;

        public LoggedUser()
        {
            loggedUser = new User();
        }

        public static LoggedUser GetInstance()
        {
            if(uniqueInstance == null)
                uniqueInstance = new LoggedUser();

            return uniqueInstance;
        }

        public User GetUser()
        {
            return loggedUser;
        }

        public void LogUser(User user)
        {
            loggedUser = user;
        }
    }
}
