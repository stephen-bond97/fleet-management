using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        User GetUser(int id);
        User GetUserByEmail(string email);
        IList<User> GetUsers();
        User UpdateUser(User user);
        bool DeleteUser(int id);
        User Register(string firstName, string lastName, string email, string password, Role role);
    }
}