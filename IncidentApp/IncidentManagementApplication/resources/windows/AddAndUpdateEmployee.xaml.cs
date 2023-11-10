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
using Service;

namespace IncidentManagementApplication.resources.windows
{
    /// <summary>
    /// Interaction logic for AddAndUpdateEmployee.xaml
    /// </summary>
    public partial class AddAndUpdateEmployee : Window
    {
        private string type;
        private User editingUser;
        private UserService userService;

        public AddAndUpdateEmployee(string type, User editingUser)
        {
            InitializeComponent();
            this.type = type;
            this.editingUser = editingUser;
            userService = new UserService();
            prepareView();
        }

        private void prepareView()
        {
            cbLocation.ItemsSource = Enum.GetValues(typeof(Location));
            cbRole.ItemsSource = Enum.GetValues(typeof(Role));

            if (type == "add")
            {
                btAddAndUpdate.Content = "Add";
                txtID.Text = userService.getNewID().ToString();
            }
            else if (type == "update")
            {
                btAddAndUpdate.Content = "Update";
                importData();
            }
        }

        private void importData()
        {
            txtID.Text = editingUser.employeeId.ToString();
            txtFirstName.Text = editingUser.firstName;
            txtLastName.Text = editingUser.lastName;
            txtEmail.Text = editingUser.email;
            cbLocation.SelectedItem = editingUser.Location;
            txtUserName.Text = editingUser.username;
            txtPassword.Password = editingUser.password;
            cbRole.SelectedItem = editingUser.Role;
        
        }

        private void btAddAndUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkBoxes())
                {
                    if (type == "add")
                    {
                        userService.CreateUser(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                            (Location)cbLocation.SelectedItem, txtPassword.Password, int.Parse(txtID.Text),
                            (Role)cbRole.SelectedItem);
                        MessageBox.Show("User added");
                    }

                    else
                    {
                        userService.updateUser(getUser());
                        MessageBox.Show("User updated");
                    }
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please fill all the fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool checkBoxes()
        {
            if(txtFirstName.Text.Length>0 && txtLastName.Text.Length>0 && txtEmail.Text.Length>0 && txtUserName.Text.Length>0 && txtPassword.Password.Length>0)
                return true;
            else
                return false;
        }

        private User getUser()
        {
                       return new User(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text,
                                          (Location)cbLocation.SelectedItem, txtPassword.Password, int.Parse(txtID.Text),
                                                         (Role)cbRole.SelectedItem);
        }
    }
}
