﻿using DAL;
using Model;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Service;

public class LoginService
{
    private readonly DAO dao;

    public LoginService()
    {
        this.dao = new DAO();
    }

    public string ResetUserPassword(string email)
    {
        string newPassword = GenerateSecurePassword();
        string resetToken = Guid.NewGuid().ToString();
        dao.StorePasswordResetToken(email, resetToken);
        SendPasswordEmailWithToken(email, resetToken);
        return resetToken;
    }


    private void SendPasswordEmailWithToken(string email, string resetToken)
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
            Body = $"To reset your password,copy paste the reset token:{resetToken}",
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
        User user = dao.FindUserByUsername(username);
        if (user != null)
        {
            return user.VerifyPassword(password);
        }

        return false;
    }

    public bool VerifyResetToken(string email, string resetToken)
    {
        return dao.VerifyResetToken(email, resetToken);
    }

    public bool ResetUserPassword(string email, string newPassword, string resetToken)
    {
        if (VerifyResetToken(email, resetToken))
        {
            
            return dao.UpdateUserPassword(email, newPassword);
        }

        return false;
    }

}