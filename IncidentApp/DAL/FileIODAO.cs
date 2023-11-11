using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class FileIODAO
    {
        // Individual Functionality 2 - Archiving Database - Ignas
        private const string fileName = "backupDB.txt";
        private const string filePath = "../../../../";
        private string fileCompletePath = filePath + fileName;
        private TicketDAO ticketDAO;
        private UserDAO userDAO;
        List<Ticket> tickets;
        List<User> users;

        public FileIODAO() { 
            this.ticketDAO = new TicketDAO();
            this.userDAO = new UserDAO();
        }

        public void createBackup()
        {
            setUpdatedData();

            if (File.Exists(fileCompletePath))
            {
                Console.WriteLine($"Overwriting data file: {fileCompletePath}");
            }
            writeFile(fileCompletePath);
            return;
        }

        private void writeFile(string filePath)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {

                streamWriter.WriteLine("#Tickets: ");
                foreach (Ticket ticket in tickets)
                {
                    streamWriter.WriteLine(ticket.ToString());
                }

                streamWriter.WriteLine("#Employees: ");
                foreach (User user in users)
                {
                    streamWriter.WriteLine(user.ToString());
                }

                streamWriter.Close();
            }
        }

        private void setUpdatedData()
        {
            tickets = ticketDAO.getTickets();
            users = userDAO.getAllUsers();
        }
    }
}
