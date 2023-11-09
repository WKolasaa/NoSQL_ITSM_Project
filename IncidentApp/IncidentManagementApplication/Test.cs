using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Service;

namespace IncidentManagementApplication
{
    internal class Test
    {
        public Test()
        {
            DAOService daoService = new DAOService();
            string username = "exampleUser";
            string firstName = "John";
            string lastName = "Doe";
            string email = "salman.kanjj@gmail.com";
            Location location = Location.Amsterdam;
            string password = "securepassword123";
            int employeeId = 12345;
            Role role = Role.RegularEmployee;

            daoService.CreateUser(username, firstName, lastName, email, location, password, employeeId, role);
        }
    }


}

