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
using IncidentManagementApplication.resources.windows;

namespace IncidentManagementApplication.windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginService loginService;
        public LoginWindow()
        {
            InitializeComponent();
            loginService = new LoginService();
        }

        
        private void Button_ForgotPassword(object sender, RoutedEventArgs e)
        {
            ForgetPasswordWindow forgetPassword=new ForgetPasswordWindow();
            forgetPassword.Show();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = passwordtxt.Password;

            if (loginService.CheckUserCredentials(username, password))
            {
                
                
                MainWindow main=new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to login");
            }
        }
    }

 
}
