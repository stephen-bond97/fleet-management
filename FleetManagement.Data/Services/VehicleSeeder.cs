using FleetManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Services
{
    public static class VehicleSeeder
    {
        public static void Seed(IVehicleService svc)
        {
            svc.Initialise();

            var vehicle1 = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "Polo",
                Year = 1958,
                BodyType = BodyType.Hatchback,
                CubicCentimeter = 1500,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 5,
                Registration = "H4H4 W4NG",
                TransmissionType = TransmissionType.Manual
            });

            var vehicle2 = svc.AddVehicle(new Vehicle
            {
                Make = "BMW",
                Model = "S Class",
                Year = 2005,
                BodyType = BodyType.Saloon,
                CubicCentimeter = 1800,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 4,
                Registration = "I H4T3 DR1V1NG",
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
    }
}
