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

namespace IncidentManagementApplication.resources.windows
{
    /// <summary>
    /// Interaction logic for ForgetPasswordWindow.xaml
    /// </summary>
    public partial class ForgetPasswordWindow : Window
    {
        private string email;
        private string resetToken;
        LoginService loginService = new LoginService();
        public ForgetPasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_ResetPassword(object sender, RoutedEventArgs e)
        {
            email = txtEmail.Text;

            resetToken = loginService.ResetUserPassword(email);

            MessageBox.Show("A token has been sent to your email address.");

        }

        private void Button_ConfirmReset(object sender, RoutedEventArgs e)
        {
            resetToken = txtToken.Text;
            string newPassword = txtNewPassword.Password;
            string confirmedPassword = txtConfirmPassword.Password;
            if (newPassword != confirmedPassword)
            {
                MessageBox.Show("Passwords do not match. Please enter matching passwords.");
                return;
            }

           
            bool tokenVerified = loginService.VerifyResetToken(email, resetToken);

            if (tokenVerified)
            {
                
                bool passwordUpdated = loginService.ResetUserPassword(email, newPassword, resetToken);

                if (passwordUpdated)
                {
                    MessageBox.Show("Password reset successfully.");
                    
                }
                else
                {
                    MessageBox.Show("Password reset failed. Please try again later.");
                }
            }
            else
            {
               
                MessageBox.Show("Invalid or expired token. Please request a new token.");
            }
        }


    }



}


