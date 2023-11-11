using Service;
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
using Model;
using System.Collections.ObjectModel;

namespace IncidentManagementApplication.resources.windows
{
    /// <summary>
    /// Interaction logic for TicketEscalation.xaml
    /// </summary>
    public partial class TicketEscalation : Window
    {
        // Ticket Escalation/Close - Ignas
        private TicketService tService;

        private Ticket currentTicket;
        private ObservableCollection<Ticket> ticketList;

        public TicketEscalation(ObservableCollection<Ticket> ticketList, Ticket selectedTicket)
        {
            InitializeComponent();
            tService = new TicketService();
            this.ticketList = ticketList;
            this.currentTicket = selectedTicket;
        }

        public void btnCloseTicket_Click(object sender, RoutedEventArgs e)
        {
            tService.closeTicket(currentTicket);
            ticketList.Remove(currentTicket);
            this.Close();
        }
        
        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
