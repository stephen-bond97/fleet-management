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

        public User GetUser(int id)
        {
            return db.Users
                .FirstOrDefault(u => u.Id == id);
        }

        public IList<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User UpdateUser(User updated)
        {
            var user = this.GetUser(updated.Id);

            // if the user isn't found, return null so that we know nothing was done
            if (user == null)
            {
                return null;
            }

            // if the user is found, map the new values to the existing user properties
            user.FirstName = updated.FirstName;
            user.LastName = updated.LastName;
            user.Email = updated.Email;
            user.Role = updated.Role;

            db.SaveChanges();

            // return the updated user so that the caller knows an action was performed
            return user;
        }

        public bool DeleteUser(int id)
        {
            var u = this.GetUser(id);

            // if the user isn't found, return false so that we know nothing was done
            if (u == null)
            {
                return false;
            }

            // remove the user that was found from the db users collection
            db.Users.Remove(u);
            db.SaveChanges();

            // return true so that the caller knows the user has been removed
            return true;
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
        }

        public User Register(string firstName, string lastName, string email, string password, Role role)
        {
            // check that the user does not already exist (unique user email)
            var exists = GetUserByEmail(email);
            if (exists != null)
            {
                return null;
            }

            // Custom Hasher used to encrypt the password before storing in database
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = Hasher.CalculateHash(password),
                Role = role
            };

            db.Users.Add(user);
            db.SaveChanges();

            // return the created user
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

    }
}
