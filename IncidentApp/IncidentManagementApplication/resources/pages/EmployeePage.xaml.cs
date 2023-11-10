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
using IncidentManagementApplication.resources.windows;
using Model;
using Service;

namespace IncidentManagementApplication.pages
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private UserService userServices;
        private User selectedUser;

        public EmployeePage()
        {
            InitializeComponent();
            userServices = new UserService();
            importEmployees();
        }

        private void importEmployees()
        {
            try
            {
                List<User> users = userServices.getAllUsers();

                lvUsers.ItemsSource = users;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAndUpdateEmployee addAndUpdateEmployee = new AddAndUpdateEmployee("add", null);
            addAndUpdateEmployee.Show();
        }

        private void btUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser != null)
            {
                AddAndUpdateEmployee addAndUpdateEmployee = new AddAndUpdateEmployee("update", selectedUser);
                addAndUpdateEmployee.Show();
            }
            else
            {
                MessageBox.Show("Please select a user to update");
            }
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedUser != null)
                {
                    userServices.removeUser(selectedUser.employeeId);
                    importEmployees();
                }
                else
                {
                    MessageBox.Show("Please select a user to remove");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = (User)lvUsers.SelectedItem;
        }
    }
}
