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

namespace IncidentManagementApplication.resources.frames
{
    /// <summary>
    /// Interaction logic for NavigationFrame.xaml
    /// </summary>
    public partial class NavigationFrame : Page
    {
        FileIOService fileIOService;

        MainWindow currentWindow;
        LoggedUser _loggedUser;

        public NavigationFrame()
        {
            InitializeComponent();
            fileIOService = new FileIOService();
            _loggedUser = LoggedUser.GetInstance();
            if (_loggedUser.GetUser().Role == Model.Role.RegularEmployee)
            {
                btnTxtCreateTicket.Text = "Create Ticket";
                btnCreateEmployee.Visibility = Visibility.Hidden;
                btnCloseTickets.Visibility = Visibility.Hidden;
            }
            else
            {
                btnTxtCreateTicket.Text = "Ticket Management";
                btnCreateEmployee.Visibility = Visibility.Visible;
                btnCloseTickets.Visibility = Visibility.Visible;
            }
            this.currentWindow = GetCurrentWindow();
            
        }

        // Dashboard - Rienat
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/DashboardPage.xaml", UriKind.Relative);
        }

        // Ticket create - Ignas
        private void btnCreateTicket_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/TicketsPage.xaml", UriKind.Relative);
        }

        // Create employee - Wojciech
        private void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/EmployeePage.xaml", UriKind.Relative);
        }

        // Close Tickets - Ignas
        private void btnCloseTickets_Click(object sender, RoutedEventArgs e)
        {
            currentWindow.frameMain.Source = new Uri("../pages/TicketsClosePage.xaml", UriKind.Relative);
        }

        // Individual Functionality 2 - Archiving Database - Ignas
        private void btnDataBackup_Click(object sender, RoutedEventArgs e)
        {
            fileIOService.createBackup();
        }

        private MainWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        }
    }
}
