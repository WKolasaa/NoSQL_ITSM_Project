using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Service;

namespace IncidentManagementApplication.pages
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        TicketService tService;

        public TicketPage()
        {
            InitializeComponent();
            tService = new TicketService();
        }

        // Ticket creation - Ignas
        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = getTicket();
            createTicket(ticket);
        }

        private void createTicket(Ticket ticket)
        {
            tService.createNewTicket(ticket);
        }

        private Ticket getTicket()
        {
            int incidentId = int.Parse(txtBoxIncidentId.Text);
            int incidentType = int.Parse(txtBoxIncidentType.Text);
            string incidentReporter = txtBoxIncidentReporter.Text;
            string incidentDesc = txtBoxIncidentDescription.Text;
            Incident incident = new Incident(incidentId, incidentType, incidentReporter, incidentDesc);
            int ticketId = int.Parse(txtBoxTicketId.Text);
            int ticketSeverity = int.Parse(txtBoxTicketSeverity.Text);
            int ticketStatus = int.Parse(txtBoxTicketStatus.Text);
            Ticket ticket = new Ticket(ticketId, ticketSeverity, ticketStatus, DateTime.Now, DateTime.Now, incident);
            return ticket;
        }

        private void btnEditTicket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
