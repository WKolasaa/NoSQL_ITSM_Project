using MongoDB.Bson;
using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;

namespace Model;

public class User
{

        [BsonElement("firstName")]
        public string firstName { get; private set; }

        [BsonElement("lastName")]
        public string lastName { get; private set; }

        [BsonElement("email")]
        public string email { get; private set; }

        [BsonElement("location")]
        public Location Location { get; private set; }

        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int employeeId { get; private set; }

        [BsonElement("username")]
        public string username { get; private set; }

        [BsonElement("password")] public string password { get; set; }


         [BsonElement("role")]
         public Role Role { get; private set; }


        public User(string username, string firstName, string lastName, string email, Location location, string password, int employeeId, Role role)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.Location = location;
            this.username = username;
             SetPassword(password); 
            this.employeeId = employeeId; 
            this.Role = role;
        }

        public void SetPassword(string plainPassword)
        {
         password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }
        public bool VerifyPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, this.password);
        }

        public User()
        {
            
        }



}