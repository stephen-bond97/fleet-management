using FleetManagement.Data.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FleetManagement.Data.Test.Models 
{
    public class UserTests : BaseTestModel
    {
        [Fact]
        public void User_FirstName_Populated_Validates_Successfully()
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void User_FirstName_Empty_Returns_Validation_Error(string input)
        {
            // Arrange
            var user = new User
            {
                FirstName = input,
                LastName = "alsotest",
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("FirstName");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void User_LastName_Populated_Validates_Successfully()
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void User_LastName_Empty_Returns_Validation_Error(string input)
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = input,
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("LastName");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void User_Email_Populated_Validates_Successfully()
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null, "required")]
        [InlineData("", "required")]
        [InlineData(" ", "required")]
        [InlineData("wrongemail@", "not a valid e-mail address")]
        public void User_Email_Empty_Returns_Validation_Error(string input, string expectedError)
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = input,
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Email");
            validationResult.ErrorMessage.Should().Contain(expectedError);
        }

        [Fact]
        public void User_Password_Populated_Validates_Successfully()
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = "test@test.com",
                Password = "testpass"
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null, "required")]
        [InlineData("", "required")]
        [InlineData(" ", "required")]
        [InlineData("four", "minimum length")]
        public void User_Password_Empty_OR_Too_Short_Returns_Validation_Error(string input, string expectedError)
        {
            // Arrange
            var user = new User
            {
                FirstName = "test",
                LastName = "alsotest",
                Email = "test@test.com",
                Password = input
            };

            // Act
            var validationResults = ValidateModel(user);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Password");
            validationResult.ErrorMessage.Should().Contain(expectedError);
        }
    }
}
