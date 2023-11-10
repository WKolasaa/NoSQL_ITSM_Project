using DAL;
using Model;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Service;

public class LoginService
{
    private readonly UserDAO userDAO;
    private User loggedUser;

    public LoginService()
    {
        this.userDAO = new UserDAO();
    }

    public string ResetUserPassword(string email)
    {
        string newPassword = GenerateSecurePassword();
        string resetToken = Guid.NewGuid().ToString();
        userDAO.StorePasswordResetToken(email, resetToken);
        SendPasswordEmailWithToken(email, resetToken);
        return resetToken;
    }


    private void SendPasswordEmailWithToken(string email, string resetToken) // additional individual functionality
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("blueoceanwaves247@gmail.com", "jcnn tfrc ovrn kwmj"),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("blueoceanwaves247@gmail.com"),
            Subject = "Password reset",
            Body = $"Dear User,<br><br>" +
                   $"You have requested to reset your password. Please use the following token to proceed with the process:<br><br>" +
                   $"<strong>{resetToken}</strong><br><br>" +
                   $"If you did not request a password reset, please ignore this email or contact our support team.<br><br>" +
                   $"Best regards,<br>" +
                   $"Garden Group",
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        client.Send(mailMessage);
    }

    private string GenerateSecurePassword(int passwordLength = 12)
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string number = "0123456789";
        const string nonAlphanumeric = "!@#$%^&*?_-";

        string allChars = upper + lower + number + nonAlphanumeric;
        byte[] data = new byte[passwordLength];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(data);
        }

        StringBuilder result = new StringBuilder(passwordLength);
        foreach (byte b in data)
        {
            result.Append(allChars[b % (allChars.Length)]);
        }

        return result.ToString();
    }

    public bool CheckUserCredentials(string username, string password)
    {
        User user = userDAO.FindUserByUsername(username);
        if (user != null)
        {
            loggedUser = user;
            return user.VerifyPassword(password);
        }

        return false;
    }

    public bool VerifyResetToken(string email, string resetToken)
    {
        return userDAO.VerifyResetToken(email, resetToken);
    }

    public bool ResetUserPassword(string email, string newPassword, string resetToken)
    {
        if (VerifyResetToken(email, resetToken))
        {
            
            return userDAO.UpdateUserPassword(email, newPassword);
        }

        return false;
    }

    public User returnUser()
    {
        return loggedUser;
    }

}