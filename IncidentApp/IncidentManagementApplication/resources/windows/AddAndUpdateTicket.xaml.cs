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
        private LoggedUser _loggedUser;

        public AddAndUpdateTicket(string Type, Ticket ticket)
        {
            InitializeComponent();
            type = Type;
            currentTicket = ticket;
            userService = new UserService();
            ticketService = new TicketService();
            _loggedUser = LoggedUser.GetInstance();
            prepareView();
        }

        private void prepareView()
        {
            cbSeverity.ItemsSource = Enum.GetValues(typeof(Severity));
            cbStatus.ItemsSource = Enum.GetValues(typeof(Status));
            cbType.ItemsSource = Enum.GetValues(typeof(IncidentType));

            if (_loggedUser.GetUser().Role == Role.RegularEmployee)
            {
                cbReporter.Items.Add(_loggedUser.GetUser().username);
                cbReporter.SelectedIndex = 0;
                cbReporter.IsEnabled = false;
                dpCreated.IsEnabled = false;
                dpUpdated.IsEnabled = false;
            }
            else
            {
                foreach (var User in userService.getAllUsers())
                {
                    cbReporter.Items.Add(User.username);
                }
            }

            if (type == "Add")
            {
                btAddUpdate.Content = "Add";
                txtID.Text = (ticketService.getLastTicketID()+1).ToString();
                dpCreated.SelectedDate = DateTime.Now;
                dpUpdated.SelectedDate = DateTime.Now;
            }
            else if (type == "Update")
            {
                btAddUpdate.Content = "Update";
                importData();
            }
        }

        private void importData()
        {
            txtID.Text = currentTicket.Id.ToString();
            cbSeverity.SelectedItem = currentTicket.Severity;
            cbStatus.SelectedItem = currentTicket.Status;
            cbReporter.SelectedItem = currentTicket.Incident.Reporter;
            cbType.SelectedItem = currentTicket.Incident.Type;
            txtDescription.Text = currentTicket.Incident.Description;
            dpCreated.SelectedDate = currentTicket.DateCreated;
            dpUpdated.SelectedDate = DateTime.Now;
        }

        private void btAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (type == "Add")
                {
                    Ticket ticket = createTicket();
                    ticketService.createNewTicket(ticket);
                    MessageBox.Show("Ticket added");
                }
                else if (type == "Update")
                {
                    Ticket ticket = createTicket();
                    ticketService.updateTicket(ticket);
                    MessageBox.Show("Ticket updated");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
