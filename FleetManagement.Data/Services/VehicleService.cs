using FleetManagement.Data.Models;
using FleetManagement.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Data.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext db;

        public VehicleService(DataContext dataContext)
        {
            db = dataContext;
        }

        public Vehicle GetVehicle(int id)
        {
            return db.Vehicles
                .Include(v => v.MOTRecords)
                .FirstOrDefault(v => v.Id == id);
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

        public MOTRecord AddMOTRecord(int vehicleId, MOTRecord result)
        {
            var vehicle = this.GetVehicle(vehicleId);

            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle {vehicleId} not found.");
            }

            var newResult = new MOTRecord
            {
                Date = result.Date,
                EngineerName = result.EngineerName,
                Mileage = result.Mileage,
                Outcome = result.Outcome,
                Report = result.Report
            };

            vehicle.MOTRecords.Add(newResult);

            var mostRecentMOTDate = newResult.Date;
            foreach (var record in vehicle.MOTRecords)
            {
                if (record.Date > mostRecentMOTDate)
                { 
                    mostRecentMOTDate = record.Date;
                }
            }

            // update vehicle's next mot date with this record's date + 1 year
            vehicle.NextMOTDate = mostRecentMOTDate.AddYears(1);
            db.SaveChanges();

            return newResult;
        }

        public MOTRecord GetMOTRecord(int resultId)
        {
            return db.MOTRecords
                  .FirstOrDefault(m => m.Id == resultId);
        }

        public MOTRecord UpdateMOTRecord(MOTRecord updated)
        {
            var result = this.GetMOTRecord(updated.Id);
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

        public bool DeleteResult(int resultId)
        {
            var r = GetMOTRecord(resultId);
            if (r == null)
            {
                return false;
            }
            db.MOTRecords.Remove(r);
            db.SaveChanges();
            return true;
        }

        public Dictionary<string, int> GetVehicleTypeSummary()
        {
            var summary = new Dictionary<string, int>();

            var bodyTypes = this.db.Vehicles
                .GroupBy(v => v.BodyType)
                .Select(v => new { BodyType = v.Key, Count = v.Count() });

            foreach (var bodyType in bodyTypes)
            {
                summary.Add(bodyType.BodyType.ToString(), bodyType.Count);
            }

            return summary;
        }

        public Dictionary<string, int> GetMOTSeveritySummary()
        {
            var summary = new Dictionary<string, int>();

            var outcomes = this.db.MOTRecords
                .GroupBy(v => v.Outcome)
                .Select(v => new { Outcome = v.Key, Count = v.Count() });

            foreach (var outcome in outcomes)
            {
                summary.Add(outcome.Outcome.ToString(), outcome.Count);
            }

            return summary;
        }

        public IList<Vehicle> GetVehiclesWithUpcomingTest()
        {
            var vehicles = this.db.Vehicles.Where(v => v.NextMOTDate < DateTime.UtcNow.AddMonths(3));

            return vehicles.ToList();
        }
    }
}
