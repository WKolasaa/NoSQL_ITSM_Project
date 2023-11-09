using DAL;
using Model;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Service
{
    public class DAOService
    {
        private DAO dao;

        public DAOService()
        {
            this.dao = new DAO();
        }

        public void getEmployees()
        {
            dao.getEmployees();
        }

        public void CreateUser(string username, string firstName, string lastName, string email, Location location,
            string password, int employeeId, Role role)
        {
            User newUser = new User(username, firstName, lastName, email, location, password, employeeId, role);
            dao.AddUser(newUser, password);
        }

       
    }
}
