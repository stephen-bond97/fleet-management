using FleetManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Services
{
    public static class Seeder
    {
        public static void Seed(IVehicleService vehicleService, IUserService userService)
        {
            SeedVehicles(vehicleService);
            SeedUsers(userService);
        }

        private static void SeedVehicles(IVehicleService svc)
        {
            var record = new MOTRecord
            {
                Date = DateTime.UtcNow,
                EngineerName = "John",
                Mileage = 15000,
                Outcome = VehicleTestOutcome.MinorDefect,
                Report = "Break light failure"
            };

            var vehicle1 = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "Polo",
                Year = 1958,
                BodyType = BodyType.Hatchback,
                CubicCentimeter = 1500,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 5,
                Registration = "B4QM 6WP",
                TransmissionType = TransmissionType.Manual
            });

            svc.AddMOTRecord(vehicle1.Id, record);

            var vehicle2 = svc.AddVehicle(new Vehicle
            {
                Make = "BMW",
                Model = "S Class",
                Year = 2005,
                BodyType = BodyType.Saloon,
                CubicCentimeter = 1800,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 4,
                Registration = "9UWM P9S",
                TransmissionType = TransmissionType.Manual
            });

            var vehicle3 = svc.AddVehicle(new Vehicle
            {
                Make = "Audi",
                Model = "A6",
                Year = 2012,
                BodyType = BodyType.Saloon,
                CubicCentimeter = 1800,
                FuelType = FuelType.Petrol,
                NumberOfDoors = 4,
                Registration = "B5A1 3AN",
                TransmissionType = TransmissionType.Automatic
            });
        }

        private static void SeedUsers(IUserService svc)
        {
            var u1 = svc.Register("David", "Bowie", "dbowie@fm.com", "dbowie", Role.Guest);
            var u2 = svc.Register("Sean", "Bean", "sbean@fm.com", "sbean", Role.Admin);
            var u3 = svc.Register("Timothy", "Awser", "tawser@fm.com", "tawser", Role.Manager);
        }
    }
}
