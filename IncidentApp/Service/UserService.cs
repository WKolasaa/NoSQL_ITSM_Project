using DAL;
using Model;

namespace Service;

public class UserService
{
    // Salman

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

    // Wojtek

    public List<User> getAllUsers()
    {
        return _userDAO.getAllUsers();
    }

    public void removeUser(int employeeId)
    {
        _userDAO.removeUser(employeeId);
    }

    public void updateUser(User user)
    {
        _userDAO.updateUser(user);
    }

    public int getNewID()
    {
        List<User> users = getAllUsers();
        int max = 0;
        foreach (var user in users)
        {
            if (user.employeeId > max)
            {
                max = user.employeeId;
            }
        }

        return max + 1;
    }
}