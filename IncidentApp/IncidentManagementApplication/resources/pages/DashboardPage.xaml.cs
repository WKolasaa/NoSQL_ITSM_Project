using DAL;
using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IncidentManagementApplication.pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        TicketService service = new TicketService();
        public DashboardPage()
        {
            InitializeComponent();
            TicketsList.Visibility = Visibility.Hidden;

            double general_amount = service.getTicketsAmount();
            double open_ticket_amount = service.getOpenedTicketsAmount();
            double resolved_amount = service.getResolvedTicketsAmount();
            double closed_amount = service.getClosedTicketsAmount();

            /*lblUnresolved.Content = $"Opened tickets: {open_ticket_amount.ToString()}";
            lblNoResolv.Content = $"Resolved tickets:{resolved_amount.ToString()}";
            lblDeadline.Content = $"Closed tickets:{closed_amount.ToString()}";*/


            IncidentCalculation(open_ticket_amount, resolved_amount, closed_amount, general_amount);
        }

        public void IncidentCalculation(double general_amount, double open_ticket_amount, double resolved_amount, double closed_amount)
        {
            double open_ticket_amount1 = (open_ticket_amount / general_amount) * 100;
            double resolved_amount1 = (resolved_amount / general_amount) * 100;
            double closed_amount1 = (closed_amount / general_amount) * 100;

            lblUnresolved.Content = $"Opened tickets: {open_ticket_amount1.ToString()}%";
            lblNoResolv.Content = $"Resolved tickets:{resolved_amount1.ToString()}%";
            lblDeadline.Content = $"Closed tickets:{closed_amount1.ToString()}%";
        }

        private void ListBtn_Click(object sender, RoutedEventArgs e)
        {
            TicketsList.Visibility = Visibility.Visible;
        }

       
    }
}
