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
using DAL;
using IncidentManagementApplication.resources.windows;
using Model;
using Service;

namespace IncidentManagementApplication.pages
{
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsPage : Page
    {
        private LoggedUser _loggedUser;
        private Ticket currentTicket;
        private string currentUser;
        private UserService userService;
        private TicketService ticketService;

        public TicketsPage()
        {
            InitializeComponent();
            userService = new UserService();
            ticketService = new TicketService();
            prepareView();
        }

        private void prepareView()
        {
            //if (_loggedUser.GetUser().Role == Role.RegularEmployee)
            if(false)
            {
                btAdd.Visibility = Visibility.Hidden;
                btRemove.Visibility = Visibility.Hidden;
                btUpdate.Visibility = Visibility.Hidden;
                cbEmployee.IsEditable = false;
                cbEmployee.DataContext = _loggedUser.GetUser();
            }
            else
            {
                btAdd.Visibility = Visibility.Visible;
                btRemove.Visibility = Visibility.Visible;
                btUpdate.Visibility = Visibility.Visible;
                cbEmployee.IsEditable = true;
                foreach (var User in userService.getAllUsers())
                {
                    cbEmployee.Items.Add(User.username);
                }
            }
        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentUser = cbEmployee.SelectedItem.ToString();
            importDataToListView();
        }

        private void importDataToListView()
        {
            lvTickets.Items.Clear();

            List<Ticket> tickets = ticketService.getTicketsForUser(currentUser);

            foreach (var ticket in tickets)
            {
                ListViewItem li = new ListViewItem();
                li.Tag = ticket;
            }


        }

        private void lvTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvTickets.SelectedItems.Count > 0)
            {
                ListViewItem listViewItem = (ListViewItem)lvTickets.SelectedItems[0];
                currentTicket = (Ticket)listViewItem.Tag;
                txtType.Text = currentTicket.getIncident().getType().ToString();
                txtReporter.Text = currentTicket.getIncident().getReporter();
                txtDescription.Text = currentTicket.getIncident().getDesc();
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateTicket addAndUpdateTicket = new AddAndUpdateTicket("Add", new Ticket());
            addAndUpdateTicket.ShowDialog();
            importDataToListView();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateTicket addAndUpdateTicket = new AddAndUpdateTicket("Update", currentTicket);
            addAndUpdateTicket.ShowDialog();
            importDataToListView();
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            ticketService.removeTicket(currentTicket.getID());
        }
    }
}
