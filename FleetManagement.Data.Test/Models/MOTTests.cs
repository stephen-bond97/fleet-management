using FleetManagement.Data.Models;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace FleetManagement.Data.Test.Models
{
    public class MOTTests : BaseTestModel
    {
        [Fact]
        public void MOTRecord_Mileage_Populated_Validates_Successfully()
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = 25000,
                Report = "testreport",
                EngineerName = "testengineer"
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void MOTRecord_Mileage_Empty_Returns_ValidationError(int input)
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = input,
                Report = "testreport",
                EngineerName = "testengineer"
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Mileage");
            validationResult.ErrorMessage.Should().Contain("between");
        }

        [Fact]
        public void MOTRecord_Report_Populated_Validates_Successfully()
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = 25000,
                Report = "testreport",
                EngineerName = "testengineer"
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void MOTRecord_Report_Empty_Returns_ValidationError(string input)
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = 25000,
                Report = input,
                EngineerName = "testengineer"
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("Report");
            validationResult.ErrorMessage.Should().Contain("required");
        }

        [Fact]
        public void MOTRecord_Engineer_Name_Populated_Validates_Successfully()
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = 25000,
                Report = "testreport",
                EngineerName = "testengineer"
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void MOTRecord_Engineer_Name_Empty_Returns_ValidationError(string input)
        {
            // Arrange
            var mot = new MOTRecord
            {
                Mileage = 25000,
                Report = "test report",
                EngineerName = input
            };

            // Act
            var validationResults = ValidateModel(mot);

            // Assert
            validationResults.Should().NotBeNull();
            validationResults.Count().Should().Be(1);

            var validationResult = validationResults.First();

            validationResult.MemberNames.Should().Contain("EngineerName");
            validationResult.ErrorMessage.Should().Contain("required");
        }

    }
}
