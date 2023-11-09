namespace Model;

public class PasswordResetToken
{
    public string Email { get; set; }
    public string ResetToken { get; set; }
    public DateTime ExpiryTime { get; set; }
}