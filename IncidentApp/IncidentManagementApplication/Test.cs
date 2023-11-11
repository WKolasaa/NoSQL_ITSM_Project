using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Service;

namespace IncidentManagementApplication
{
    internal class Test
    {
        public Test()
        {
            UserDAO userDAO = new UserDAO();
            UserService userService = new UserService();

            // Sample data for creating users
            string username = "exampleUser";
            string firstName = "John";
            string lastName = "Doe";
            string email = "salman.kanjj@gmail.com";
            Location location = Location.Amsterdam;
            string password = "securepassword123";
            int employeeId = 12345;
            Role role = Role.RegularEmployee;

            // Create 100 users
            for (int i = 0; i < 100; i++)
            {
                // Append a unique identifier to the username to make it unique for each user
                string uniqueUsername = $"{username}_{i}";

                if (i % 2 == 0)
                {
                    role = Role.RegularEmployee;
                }
                else
                {
                    role = Role.ServiceDesk;
                }

                email = i + "@gmail.com";

                // Create and add a user to the database
                userService.CreateUser(uniqueUsername, firstName, lastName, email, location, password, userService.getNewID(), role);
            }

            Console.WriteLine("Users created successfully.");
        }
    }


}

