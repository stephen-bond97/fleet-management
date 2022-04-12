using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        User GetUserByEmail(string email);
        User Register(string name, string email, string password, Role role);
    }
}