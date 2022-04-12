using FleetManagement.Data.Models;
using FleetManagement.Data.Repository;
using Microsoft.EntityFrameworkCore;

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
            return db.Vehicles
                .Include(v => v.MOTResults)
                .First(v => v.Id == id);
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

        public bool DeleteVehicle(int id)
        {
            var v = GetVehicle(id);
            if (v == null)
            {
                return false;
            }
            db.Vehicles.Remove(v);
            db.SaveChanges();
            return true;
        }

        public Vehicle UpdateVehicle(Vehicle updated)
        {
            var vehicle = this.GetVehicle(updated.Id);
            if (vehicle == null)
            {
                return null;
            }

            vehicle.Model = updated.Model;
            vehicle.Make = updated.Make;
            vehicle.Year = updated.Year;
            vehicle.BodyType = updated.BodyType;
            vehicle.TransmissionType = updated.TransmissionType;
            vehicle.FuelType = updated.FuelType;
            vehicle.CubicCentimeter = updated.CubicCentimeter;
            vehicle.NumberOfDoors = updated.NumberOfDoors;
            vehicle.Registration = updated.Registration;

            db.SaveChanges();
            return vehicle;
        }

        public MOTResult AddMOTResult(int vehicleId, MOTResult result)
        {
            var vehicle = this.GetVehicle(vehicleId);

            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle {vehicleId} not found.");
            }

            var newResult = new MOTResult
            {
                Date = result.Date,
                EngineerName = result.EngineerName,
                Mileage = result.Mileage,
                Outcome = result.Outcome,
                Report = result.Report
            };

            vehicle.MOTResults.Add(newResult);
            db.SaveChanges();

            return newResult;
        }

        public MOTResult GetMOTResult(int resultId)
        {
            return db.MOTResults
                  .First(m => m.Id == resultId);
        }

        public MOTResult UpdateMOTResult(MOTResult updated)
        {
            var result = this.GetMOTResult(updated.Id);
            if (result == null)
            {
                return null;
            }

            result.Date = updated.Date;
            result.EngineerName = updated.EngineerName;
            result.Mileage = updated.Mileage;
            result.Outcome = updated.Outcome;
            result.Report = updated.Report;

            db.SaveChanges();
            return result;
        }
    }
}
