using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public static class Seeder
    {
        public static void Seed(IVehicleService vehicleService, IUserService userService)
        {
            SeedVehicles(vehicleService);
            SeedRecords(vehicleService);
            SeedUsers(userService);
        }

        private static void SeedVehicles(IVehicleService svc)
        {
            var vehicle1 = svc.AddVehicle(new Vehicle
            {
                Make = "Volkswagen",
                Model = "Polo",
                Year = 1998,
                BodyType = BodyType.Hatchback,
                CubicCentimeter = 1500,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 5,
                Registration = "B4QM 6WP",
                TransmissionType = TransmissionType.Manual,
                Picture = "VW_Polo.jpg"
            });

            var vehicle2 = svc.AddVehicle(new Vehicle
            {
                Make = "BMW",
                Model = "S Class",
                Year = 2005,
                BodyType = BodyType.Estate,
                CubicCentimeter = 1800,
                FuelType = FuelType.Diesel,
                NumberOfDoors = 4,
                Registration = "9UWM P9S",
                TransmissionType = TransmissionType.Manual,
                Picture = "mercedes_s-class.jpg"
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
                TransmissionType = TransmissionType.Automatic,
                Picture = "2012-audi.jpg"
            });

            var vehicle4 = svc.AddVehicle(new Vehicle
            {
                Make = "Range Rover",
                Model = "Defender",
                Year = 2015,
                BodyType = BodyType.SUV,
                CubicCentimeter = 2000,
                FuelType = FuelType.Petrol,
                NumberOfDoors = 5,
                Registration = "KDW8 6AM",
                TransmissionType = TransmissionType.Automatic,
                Picture = "defender.jpg"
            });

            var vehicle5 = svc.AddVehicle(new Vehicle
            {
                Make = "Suzuki",
                Model = "Vitara",
                Year = 2011,
                BodyType = BodyType.SUV,
                CubicCentimeter = 1700,
                FuelType = FuelType.Petrol,
                NumberOfDoors = 3,
                Registration = "OW1M 9DW",
                TransmissionType = TransmissionType.Automatic,
                Picture = "vitara.jpg"
            });

            var vehicle6 = svc.AddVehicle(new Vehicle
            {
                Make = "Vauxhall",
                Model = "Astra",
                Year = 2014,
                BodyType = BodyType.Hatchback,
                CubicCentimeter = 1400,
                FuelType = FuelType.Petrol,
                NumberOfDoors = 5,
                Registration = "GXU 2300",
                TransmissionType = TransmissionType.Manual,
                Picture = "astra.jpg"
            });

            var vehicle7 = svc.AddVehicle(new Vehicle
            {
                Make = "Citreon",
                Model = "Saxo",
                Year = 1998,
                BodyType = BodyType.Hatchback,
                CubicCentimeter = 1100,
                FuelType = FuelType.Petrol,
                NumberOfDoors = 3,
                Registration = "D0G UEM",
                TransmissionType = TransmissionType.Manual,
                Picture = "saxo.jpg"
            });
        }

        private static void SeedRecords(IVehicleService svc)
        {
            var record1 = new MOTRecord
            {
                Date = DateTime.UtcNow,
                EngineerName = "John",
                Mileage = 15000,
                Outcome = VehicleTestOutcome.MinorDefect,
                Report = "Break light failure"
            };

            var record2 = new MOTRecord
            {
                Date = DateTime.UtcNow,
                EngineerName = "Michael",
                Mileage = 200000,
                Outcome = VehicleTestOutcome.MajorDefect,
                Report = "Missing bonnet"
            };

            var record3 = new MOTRecord
            {
                Date = DateTime.UtcNow,
                EngineerName = "David",
                Mileage = 500000,
                Outcome = VehicleTestOutcome.DangerousDefect,
                Report = "Missing Car"
            };

            var record4 = new MOTRecord
            {
                Date = DateTime.UtcNow.AddYears(-1),
                EngineerName = "Sean",
                Mileage = 25000,
                Outcome = VehicleTestOutcome.DangerousDefect,
                Report = "No Wheels"
            };

            var record5 = new MOTRecord
            {
                Date = DateTime.UtcNow.AddMonths(-11),
                EngineerName = "Paul",
                Mileage = 65000,
                Outcome = VehicleTestOutcome.MajorDefect,
                Report = "No Front Lights"
            };

            var record6 = new MOTRecord
            {
                Date = DateTime.UtcNow.AddMonths(-10),
                EngineerName = "Ian",
                Mileage = 165000,
                Outcome = VehicleTestOutcome.Advisory,
                Report = "N/A"
            };

            var record7 = new MOTRecord
            {
                Date = DateTime.UtcNow,
                EngineerName = "Ian",
                Mileage = 112000,
                Outcome = VehicleTestOutcome.Pass,
                Report = "N/A"
            };

            var record8 = new MOTRecord
            {
                Date = DateTime.UtcNow.AddYears(-1),
                EngineerName = "Ian",
                Mileage = 112000,
                Outcome = VehicleTestOutcome.DangerousDefect,
                Report = "N/A"
            };

            svc.AddMOTRecord(1, record1);
            svc.AddMOTRecord(2, record2);
            svc.AddMOTRecord(3, record3);
            svc.AddMOTRecord(4, record4);
            svc.AddMOTRecord(5, record5);
            svc.AddMOTRecord(6, record6);
            svc.AddMOTRecord(7, record7);
            svc.AddMOTRecord(7, record8);
        }

        private static void SeedUsers(IUserService svc)
        {
            var u1 = svc.Register("David", "Bowie", "dbowie@fm.com", "dbowie", Role.Guest);
            var u2 = svc.Register("Sean", "Bean", "sbean@fm.com", "sbean", Role.Admin);
            var u3 = svc.Register("Timothy", "Awser", "tawser@fm.com", "tawser", Role.Manager);
        }
    }
}
