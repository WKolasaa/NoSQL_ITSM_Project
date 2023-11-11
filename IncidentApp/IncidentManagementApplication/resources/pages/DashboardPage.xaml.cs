using DAL;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace IncidentManagementApplication.pages
{
    
    public partial class DashboardPage : Page
    {
        TicketService service = new TicketService();
        List<Ticket> tickets;
        private LoggedUser _loggedUser;

        public DashboardPage()
        {
            InitializeComponent();
           

            TicketsList.Visibility = Visibility.Hidden;
            

            _loggedUser = LoggedUser.GetInstance();
            string name = _loggedUser.GetLoggedUserName();
            
            lbl1.Content = $"Dashboard {name}";

            IncidentCalculation();

            string username = _loggedUser.GetLoggedUseruserName();
            FillTicketsList(username);
            FilterBox.TextChanged += FilterBox_TextChanged;

        }


        public void IncidentCalculation()
        {
            int general_amount = service.getTicketsAmount();
            int open_ticket_amount = service.getOpenedTicketsAmount();
            int resolved_amount = service.getResolvedTicketsAmount();
            int closed_amount = service.getClosedTicketsAmount();

            double openTicketPercentage = (double)open_ticket_amount / general_amount * 100;
            double resolvedPercentage = (double)resolved_amount / general_amount * 100;
            double closedPercentage = (double)closed_amount / general_amount * 100;

            lblOpen.Content = $"Opened tickets: {openTicketPercentage:F2}%";
            lblResolv.Content = $"Resolved tickets: {resolvedPercentage:F2}%";
            lblClosed.Content = $"Closed tickets: {closedPercentage:F2}%";
        }

        public void FillTicketsList(string name)
        {
            List<Ticket> tickets = service.getTicketsForUser(name);
            TicketsList.ItemsSource = tickets;
        }

        private void ListBtn_Click(object sender, RoutedEventArgs e)
        {
            tickets = service.getTicketsForUser(_loggedUser.GetLoggedUseruserName());
            TicketsList.Visibility = Visibility.Visible;
            lblClosed.Visibility = Visibility.Hidden;
            lblResolv.Visibility = Visibility.Hidden;
            lblOpen.Visibility = Visibility.Hidden;
            ListHideBtn.Visibility = Visibility.Visible;
            ListBtn.Visibility = Visibility.Hidden;

        }

        private void ListHideBtn_Click(object sender, RoutedEventArgs e)
        {
            ListBtn.Visibility = Visibility.Visible;
            TicketsList.Visibility = Visibility.Hidden;
            lblClosed.Visibility = Visibility.Visible;
            lblResolv.Visibility = Visibility.Visible;
            lblOpen.Visibility = Visibility.Visible;

            if (ListBtn.Visibility == Visibility.Visible)
            {
                ListHideBtn.Visibility = Visibility.Hidden;
            }

        }

        

        private void UpdateListView(List<Ticket> newTickets)
        {
            TicketsList.ItemsSource = null;
            TicketsList.ItemsSource = newTickets;
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterText = FilterBox.Text.ToLower();

            if (tickets != null)
            {
                List<Ticket> filteredTickets = new List<Ticket>();

                foreach (var VARIABLE in tickets)
                {
                    if (VARIABLE.Incident.Description.ToLower().Contains(filterText))
                    {
                        filteredTickets.Add(VARIABLE);
                    }
                    else if (VARIABLE.Status.ToString().ToLower().Contains(filterText))
                    {
                        filteredTickets.Add(VARIABLE);
                    }
                    else if (VARIABLE.Id.ToString().ToLower().Contains(filterText))
                    {
                        filteredTickets.Add(VARIABLE);
                    }
                    else if (VARIABLE.Severity.ToString().ToLower().Contains(filterText))
                    {
                        filteredTickets.Add(VARIABLE);
                    }
                }

                UpdateListView(filteredTickets);
            }
        }
    }
}
