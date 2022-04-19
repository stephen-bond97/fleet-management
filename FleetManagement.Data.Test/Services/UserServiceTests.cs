using FleetManagement.Data.Models;
using FleetManagement.Data.Repository;
using FleetManagement.Data.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FleetManagement.Data.Test.Services
{
    public class UserServiceTests
    {
        private readonly IUserService svc;

        public UserServiceTests()
        {
            var context = new DataContext();

            svc = new UserService(context);

            context.Initialise();
        }

        [Fact]
        public void GetUser_Existing_Id_Should_Return_User()
        {
            // Arrange
            var user = svc.Register("test", "testlast", "test@test.com", "testpass", Role.Guest);

            // Act
            var result = svc.GetUser(user.Id);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("test");
        }

        [Fact]
        public void GetUser_NonExisting_Id_Should_Return_Null()
        {

            // Act
            var result = svc.GetUser(150);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetUsers_Should_Return_Users()
        {
            // Arrange
            var user = svc.Register("test", "testlast", "test@test.com", "testpass", Role.Guest);

            // Act
            var result = svc.GetUsers();

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void UpdateUser_Should_Update_User()
        {
            // Arrange
            var user = svc.Register("test", "testlast", "test@test.com", "testpass", Role.Guest);

            var updatedUser = new User
            {
                Id = user.Id,
                FirstName = "newtest",
                LastName = "newlast",
                Email = "new@email.com",
                Password = "newpass",
                Role = Role.Admin
            };

            // Act
            svc.UpdateUser(updatedUser);
            var result = svc.GetUser(user.Id);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("newtest");
            result.LastName.Should().Be("newlast");
            result.Email.Should().Be("new@email.com");
        }
    }
}
