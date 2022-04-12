using FleetManagement.Data.Models;
using FleetManagement.Data.Repository;
using FleetManagement.Data.Security;

namespace FleetManagement.Data.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext db;

        public UserService(DataContext dataContext)
        {
            db = dataContext;
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
        }

        public User Register(string name, string email, string password, Role role)
        {
            // check that the user does not already exist (unique user name)
            var exists = GetUserByEmail(email);
            if (exists != null)
            {
                return null;
            }

            // Custom Hasher used to encrypt the password before storing in database
            var user = new User
            {
                Name = name,
                Email = email,
                Password = Hasher.CalculateHash(password),
                Role = role
            };

            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

    }
}
