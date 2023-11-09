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
            UserService userService = new UserService();
            string username = "exampleUser";
            string firstName = "John";
            string lastName = "Doe";
            string email = "salman.kanjj@gmail.com";
            Location location = Location.Amsterdam;
            string password = "securepassword123";
            int employeeId = 12345;
            Role role = Role.RegularEmployee;

            userService.CreateUser(username, firstName, lastName, email, location, password, employeeId, role);
        }
    }


}

