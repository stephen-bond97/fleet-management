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

        [Fact]
        public void GetVehicles_Should_Return_Vehicles()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "testmake",
                Model = "testmodel",
                Registration = "LET 95N",
                CubicCentimeter = 8000
            });

            // Act
            var result = svc.GetVehicles();

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void DeleteVehicle_Should_Delete_Vehicle()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "testmake",
                Model = "testmodel",
                Registration = "LET 95N",
                CubicCentimeter = 8000
            });

            // Act
            var result = svc.DeleteVehicle(vehicle.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void DeleteVehicle_Vehicle_DoesNotExist_Should_Return_False()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "testmake",
                Model = "testmodel",
                Registration = "LET 95N",
                CubicCentimeter = 8000
            });

            // Act
            var result = svc.DeleteVehicle(100);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void UpdateVehicle_Should_Delete_Vehicle()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "testmake",
                Model = "testmodel",
                Registration = "LET 95N",
                CubicCentimeter = 8000
            });

            var updatedVehicle = new Vehicle
            {
                Id = vehicle.Id,
                Make = "test2",
                Model = "testmodel2",
                Registration = "T3ST",
                CubicCentimeter = 1000
            };

            // Act
            svc.UpdateVehicle(updatedVehicle);
            var result = svc.GetVehicle(vehicle.Id);

            // Assert
            result.Should().NotBeNull();
            result.Make.Should().Be("test2");
            result.Model.Should().Be("testmodel2");
            result.Registration.Should().Be("T3ST");
        }

        [Fact]
        public void AddMOTRecord_Should_Create_Result()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "test",
                CubicCentimeter = 5000
            });

            var record = new MOTRecord
            {
                Date = DateTime.Now,
                EngineerName = "Michael",
                Outcome = VehicleTestOutcome.MinorDefect,
                Mileage = 5000,
                Report = "test"
            };

            // Act
            svc.AddMOTRecord(vehicle.Id, record);
            var result = svc.GetVehicle(vehicle.Id);

            // Assert
            result.Should().NotBeNull();
            result.MOTRecords.Should().NotBeEmpty();

            var firstRecord = result.MOTRecords.First();

            firstRecord.Report.Should().Be("test");
        }

        [Fact]
        public void GetMOTRecord_Should_Return_Record()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "test",
                CubicCentimeter = 5000
            });

            var record = new MOTRecord
            {
                Date = DateTime.Now,
                EngineerName = "Michael",
                Outcome = VehicleTestOutcome.MinorDefect,
                Mileage = 5000,
                Report = "test"
            };

            // Act
            var added = svc.AddMOTRecord(vehicle.Id, record);
            var result = svc.GetMOTRecord(added.Id);

            // Assert
            result.Should().NotBeNull();
            result.EngineerName.Should().Be("Michael");
        }

        [Fact]
        public void DeleteMOTRecord_Should_Delete_Record()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "test",
                CubicCentimeter = 5000
            });

            var record = new MOTRecord
            {
                Date = DateTime.Now,
                EngineerName = "Michael",
                Outcome = VehicleTestOutcome.MinorDefect,
                Mileage = 5000,
                Report = "test"
            };

            // Act
            var added = svc.AddMOTRecord(vehicle.Id, record);
            var result = svc.DeleteResult(added.Id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void UpdateMOTRecord_Should_Update_Record()
        {
            // Arrange
            var vehicle = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "testCar",
                Registration = "test",
                CubicCentimeter = 5000
            });

            var record = new MOTRecord
            {
                Date = DateTime.Now,
                EngineerName = "Michael",
                Outcome = VehicleTestOutcome.MinorDefect,
                Mileage = 5000,
                Report = "test"
            };            

            // Act
            var added = svc.AddMOTRecord(vehicle.Id, record);

            var updatedRecord = new MOTRecord
            {
                Date = DateTime.Now,
                EngineerName = "Sean",
                Outcome = VehicleTestOutcome.DangerousDefect,
                Mileage = 50600,
                Report = "wrong",
                Id = added.Id,
            };

            var result = svc.UpdateMOTRecord(updatedRecord);

            // Assert
            result.Should().NotBeNull();
            result.Report.Should().Be("wrong");
        }
    }
}
