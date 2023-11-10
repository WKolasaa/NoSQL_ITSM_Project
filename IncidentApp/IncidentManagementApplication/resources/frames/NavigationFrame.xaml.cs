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

namespace IncidentManagementApplication.resources.frames
{
    /// <summary>
    /// Interaction logic for NavigationFrame.xaml
    /// </summary>
    public partial class NavigationFrame : Page
    {
        MainWindow currentWindow;

        public NavigationFrame()
        {
            InitializeComponent();
            this.currentWindow = GetCurrentWindow();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/DashboardPage.xaml", UriKind.Relative);
        }

        // Ticket create - Ignas
        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/TicketsPage.xaml", UriKind.Relative);
        }

        private void btnViewTickets_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/TicketsOverviewPage.xaml", UriKind.Relative);
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/DashboardPage.xaml", UriKind.Relative);
        }

        private void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/EmployeePage.xaml", UriKind.Relative);
        }

        private MainWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
    }
}
