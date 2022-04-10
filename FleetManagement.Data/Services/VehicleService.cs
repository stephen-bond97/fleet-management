using FleetManagement.Data.Models;
using FleetManagement.Data.Repository;

namespace FleetManagement.Data.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext db;

        public VehicleService()
        {
            db = new DataContext();
        }

        public void Initialise()
        {
            db.Initialise();
        }

        public Vehicle GetVehicle(int id)
        {
            return db.Vehicles.First(x => x.Id == id);
        }

        public IList<Vehicle> GetVehicles()
        {
            return db.Vehicles.ToList();
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            var newVehicle = new Vehicle
            {
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                BodyType = vehicle.BodyType,
                TransmissionType = vehicle.TransmissionType,
                FuelType = vehicle.FuelType,
                CubicCentimeter = vehicle.CubicCentimeter,
                NumberOfDoors = vehicle.NumberOfDoors,
                Registration = vehicle.Registration
            };

            db.Vehicles.Add(newVehicle);
            db.SaveChanges();

            return newVehicle;
        }
    }
}
