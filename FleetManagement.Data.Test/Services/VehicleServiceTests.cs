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
    public class VehicleServiceTests
    {
        private readonly IVehicleService svc;

        public VehicleServiceTests()
        {
            var context = new DataContext();

            svc = new VehicleService(context);

            context.Initialise();
        }

        [Fact]
        public void GetVehicle_Existing_Id_Should_Return_Vehicle()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "OES 9DL",
                CubicCentimeter = 5000
            });

            // Act
            var result = svc.GetVehicle(vehicle.Id);

            // Assert
            result.Should().NotBeNull();
            result.Registration.Should().Be("OES 9DL");
        }

        [Fact]
        public void GetVehicle_NonExistent_Id_Should_Return_Null()
        {
            // Act
            var result = svc.GetVehicle(100);

            // Assert
            result.Should().BeNull();
        }
    }
}
