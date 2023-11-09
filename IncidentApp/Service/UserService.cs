using DAL;
using Model;

namespace Service;

public class UserService
{
    private UserDAO _userDAO;
    public UserService()
    {
        this._userDAO=new UserDAO();
    }

    public void CreateUser(string username, string firstName, string lastName, string email, Location location,
        string password, int employeeId, Role role)
    {
        User newUser = new User(username, firstName, lastName, email, location, password, employeeId, role);
        _userDAO.AddUser(newUser, password);
    }
}