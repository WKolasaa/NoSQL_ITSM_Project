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
using System.Windows.Shapes;
using DAL;
using Model;
using Service;

namespace IncidentManagementApplication.resources.windows
{
    /// <summary>
    /// Interaction logic for AddAndUpdateTicket.xaml
    /// </summary>
    public partial class AddAndUpdateTicket : Window
    {
        private string type;
        private Ticket currentTicket;
        private UserService userService;
        private TicketService ticketService;

        public AddAndUpdateTicket(string Type, Ticket ticket)
        {
            InitializeComponent();
            type = Type;
            currentTicket = ticket;
            userService = new UserService();
            ticketService = new TicketService();
            prepareView();
        }

        private void prepareView()
        {
            cbSeverity.ItemsSource = Enum.GetValues(typeof(Severity));
            foreach (var User in userService.getAllUsers())
            {
                cbReporter.Items.Add(User.username);
            }
            cbStatus.ItemsSource = Enum.GetValues(typeof(Status));
            cbType.ItemsSource = Enum.GetValues(typeof(IncidentType));

            if (type == "Add")
            {
                btAddUpdate.Content = "Add";
            }
            else if (type == "Update")
            {
                btAddUpdate.Content = "Update";
                importData();
            }
        }

        private void importData()
        {
            txtID.Text = currentTicket.getID().ToString();
            cbSeverity.SelectedItem = currentTicket.getSeverity();
            cbStatus.SelectedItem = currentTicket.getStatus();
            cbReporter.SelectedItem = currentTicket.getIncident().getReporter();
            cbType.SelectedItem = currentTicket.getIncident().getType();
            txtDescription.Text = currentTicket.getIncident().getDesc();
            dpCreated.SelectedDate = currentTicket.getDateCreated();
            dpUpdated.SelectedDate = currentTicket.getDateUpdated();
        }

        private void btAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (type == "Add")
            {
                Ticket ticket = createTicket();
                ticketService.createNewTicket(ticket);
            }
            else if (type == "Update")
            {
                Ticket ticket = createTicket();
                //ticketService.updateTicket(ticket);
            }   
        }

        private void cbCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private Ticket createTicket()
        {
            int id = int.Parse(txtID.Text);
            Severity severity = (Severity)cbSeverity.SelectedItem;
            Status status = (Status)cbStatus.SelectedItem;
            string reporter = cbReporter.SelectedItem.ToString();
            IncidentType incidentType = (IncidentType)cbType.SelectedItem;
            string description = txtDescription.Text;
            DateTime dateCreated = dpCreated.SelectedDate.Value;
            DateTime dateUpdated = dpUpdated.SelectedDate.Value;

            Incident incident = new Incident(id, (int)incidentType, reporter, description);
            Ticket ticket = new Ticket(id, (int)severity, (int)status, dateCreated, dateUpdated, incident);
            return ticket;
        }
    }
}
