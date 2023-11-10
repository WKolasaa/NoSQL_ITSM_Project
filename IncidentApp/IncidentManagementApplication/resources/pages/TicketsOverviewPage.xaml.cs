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
    /// <summary>
    /// Interaction logic for TicketsPage.xaml
    /// </summary>
    public partial class TicketsOverviewPage : Page
    {
        private TicketService tService;
        public ObservableCollection<Ticket> Tickets { get; set; }

        public TicketsOverviewPage()
        {
            InitializeComponent();
            this.tService = new TicketService();
            Tickets = new ObservableCollection<Ticket>(tService.getNotClosedTickets());
            refreshTable();
        }

        private void refreshTable()
        {
            dataGridTickets.ItemsSource = null;
            dataGridTickets.Items.Clear();

            Tickets = new ObservableCollection<Ticket>(tService.getNotClosedTickets());
            dataGridTickets.ItemsSource = Tickets;
        }

        public void btnCloseTicket_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGridTickets.SelectedItems.Cast<Ticket>().ToList();

            foreach (var selectedItem in selectedItems)
            {
                tService.closeTicket(selectedItem);
                Tickets.Remove(selectedItem);
            }
        }
    }
}
