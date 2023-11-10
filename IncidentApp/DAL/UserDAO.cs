using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DAL;

public class UserDAO:DAO
{

    public void StorePasswordResetToken(string email, string resetToken)
    {
        var passwordResetTokensCollection = getCollection("PasswordResetTokens");



        var tokenDocument = new BsonDocument
            {
                { "Email", email },
                { "ResetToken", resetToken },
                { "ExpiryTime", DateTime.UtcNow.AddHours(1) }
            };


        passwordResetTokensCollection.InsertOne(tokenDocument);
    }

  
    

    private IMongoCollection<User> GetUserCollection()
    {
        return database.GetCollection<User>("Employees");
    }

    public void AddUser(User newUser, string password)
    {
        var usersCollection = GetUserCollection();
        newUser.SetPassword(password);
        usersCollection.InsertOne(newUser);
    }

    public bool UpdateUserPassword(string email, string newPassword)
    {
        try
        {
            var usersCollection = GetUserCollection();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            var filter = Builders<User>.Filter.Eq("email", email);
            var update = Builders<User>.Update.Set("password", hashedPassword);
            var result = usersCollection.UpdateOne(filter, update);

            if (result.ModifiedCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public User FindUserByUsername(string username)
    {
        var usersCollection = GetUserCollection();
        return usersCollection.Find<User>(user => user.username == username).FirstOrDefault();
    }
    public bool VerifyResetToken(string email, string token)
    {
        var passwordResetTokensCollection = database.GetCollection<BsonDocument>("PasswordResetTokens");

        var filter = Builders<BsonDocument>.Filter.Eq("Email", email) & Builders<BsonDocument>.Filter.Eq("ResetToken", token) & Builders<BsonDocument>.Filter.Gt("ExpiryTime", DateTime.UtcNow);
        var tokenDocument = passwordResetTokensCollection.Find(filter).FirstOrDefault();

        if (tokenDocument != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Wojtek

    public List<User> getAllUsers()
    {
        List<User> users = GetUserCollection().Find(new BsonDocument()).ToList();

        return users;
    }

    public void removeUser(int employeeId)
    {
        var usersCollection = GetUserCollection();
        var filter = Builders<User>.Filter.Eq("employeeId", employeeId);
        usersCollection.DeleteOne(filter);
    }

}