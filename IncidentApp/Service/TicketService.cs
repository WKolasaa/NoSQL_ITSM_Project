using DAL;
using Model;
using System;
using System.Collections.Generic;
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
        public void createNewTicket(Ticket ticket)
        {
            ticketDao.createNewTicket(ticket);
        }

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
    }
}
