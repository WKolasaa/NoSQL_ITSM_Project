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
        int counter = 1;
        public DashboardPage()
        {
            InitializeComponent();
            TicketsList.Visibility = Visibility.Hidden;
            IncidentCalculation();
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

            lblUnresolved.Content = $"Opened tickets: {openTicketPercentage:F2}%";
            lblNoResolv.Content = $"Resolved tickets: {resolvedPercentage:F2}%";
            lblDeadline.Content = $"Closed tickets: {closedPercentage:F2}%";
        }

        private void ListBtn_Click(object sender, RoutedEventArgs e)
        {
            TicketsList.Visibility = Visibility.Visible;
            lblDeadline.Visibility = Visibility.Hidden;
            lblNoResolv.Visibility = Visibility.Hidden;
            lblUnresolved.Visibility = Visibility.Hidden;
        }

       
    }
}
