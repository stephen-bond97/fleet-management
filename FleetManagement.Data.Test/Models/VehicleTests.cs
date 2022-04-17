using FleetManagement.Data.Models;
using FleetManagement.Data.Test.Models;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace FleetManagement.Data.Tests.Models
{
    public class VehicleTests : BaseTestModel
    {
        [Fact]
        public void Vehicle_Make_Populated_Validates_Successfully()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Vehicle_Make_Empty_ReturnsValidationError(string input)
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = input,
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Make");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void Vehicle_Model_Populated_Validates_Successfully()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Vehicle_Model_Empty_ReturnsValidationError(string input)
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = "test",
                Model = input,
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Model");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void Vehicle_Registration_Populated_Validates_Successfully()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Vehicle_Registration_Empty_ReturnsValidationError(string input)
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Make = "test",
                Model = "test car",
                Registration = input,
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Registration");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void Vehicle_Year_Within_Range_Validates_Successfully()
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000,
                Year = DateTime.UtcNow.Year
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        [InlineData(1900)]
        [InlineData(2500)]
        public void Vehicle_Year_Outside_Range_ReturnsValidationError(int year)
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000,
                Year = year
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Year");
            validationResult.ErrorMessage.Should().Contain("must be between");
        }

        [Fact]
        public void Vehicle_CCs_Within_Range_Validates_Successfully()
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        [InlineData(500)]
        [InlineData(100000)]
        public void Vehicle_CCs_Outside_Range_ReturnsValidationError(int cubic)
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = cubic
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("CubicCentimeter");
            validationResult.ErrorMessage.Should().Contain("must be between");
        }

        [Fact]
        public void Vehicle_Doors_Within_Range_Validates_Successfully()
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000,
                NumberOfDoors = 1
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().BeEmpty();
            validationResults.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void Vehicle_Doors_Outside_Range_ReturnsValidationError(int doors)
        {
            var vehicle = new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000,
                NumberOfDoors = doors
            };

            // Act
            var validationResults = ValidateModel(vehicle);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("NumberOfDoors");
            validationResult.ErrorMessage.Should().Contain("must be between");
        }
    }
}