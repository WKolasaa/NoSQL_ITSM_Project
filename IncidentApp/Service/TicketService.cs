using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class TicketService
    {
        // TicketDao setup - Ignas
        private TicketDAO ticketDao;

        public TicketService() { 
            this.ticketDao = new TicketDAO();
          
        }

        // Ticket creation - Ignas
        public int getLastTicketID()
        {
            return ticketDao.getLastTicketID();
        }

        public void createNewTicket(Ticket ticket)
        {
            ticketDao.createNewTicket(ticket);
        }

        // Get tickets - Ignas 
        public List<Ticket> getTickets()
        {
            return ticketDao.getTickets();
        }

        public List<Ticket> getNotClosedTickets()
        {
            return ticketDao.getNotClosedTickets();
        }

        // Dashboard display - Rienat
        public int getTicketsAmount()
        {
            int amount = 0;
            amount = ticketDao.getTicketsCount();
            return amount;
        }

        public int getOpenedTicketsAmount()
        {
            int amount = 0;
            amount = ticketDao.getOpenedTicketCount();
            return amount;
        }

        public int getResolvedTicketsAmount()
        {
            int amount = 0;
            amount = ticketDao.getResolvedTicketCount();
            return amount;
        }

        public int getClosedTicketsAmount()
        {
            int amount = 0;
            amount = ticketDao.getClosedTicketCount();
            return amount;
        }

        // Wojtek

        public List<Ticket> getTicketsForUser(string username)
        {
            return ticketDao.GetTicketsForUser(username);
        }

        public void removeTicket(int id)
        {
            ticketDao.removeTicket(id);
        }

        public void updateTicket(Ticket ticket)
        {
            ticketDao.updateTicket(ticket);
        }



        public void closeTicket(Ticket selectedItem)
        {
            ticketDao.closeTicket(selectedItem);
        }
    }
}
