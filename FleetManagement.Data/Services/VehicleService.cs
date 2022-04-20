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

        public IList<Vehicle> GetVehicles(string registration, string make, string model)
        {
            // created a query so that we can conditionally add where clauses
            var vehicles = from v in db.Vehicles select v;

            // if search term (reg/make/model) is populated, add the where clause to the original query
            if (registration != null)
            {
                vehicles = vehicles.Where(v => v.Registration.ToLower().Contains(registration.ToLower()));
            }

            if (make != null)
            {
                vehicles = vehicles.Where(v => v.Make.ToLower().Contains(make.ToLower()));
            }

            if (model != null)
            {
                vehicles = vehicles.Where(v => v.Model.ToLower().Contains(model.ToLower()));
            }

            // execute the built query to find the vehicles that match our search terms
            return vehicles.ToList();
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
                Registration = vehicle.Registration,
                Picture = vehicle.Picture
            };

            db.Vehicles.Add(newVehicle);
            db.SaveChanges();

            return newVehicle;
        }

        public bool DeleteVehicle(int id)
        {
            var v = GetVehicle(id);

            // if the vehicle isn't found, return false
            if (v == null)
            {
                return false;
            }

            // remove the vehicle that was found from the db vehicle collection
            db.Vehicles.Remove(v);
            db.SaveChanges();

            // return true so that the caller knows the vehicle has been removed
            return true;
        }

        public Vehicle UpdateVehicle(Vehicle updated)
        {
            var vehicle = this.GetVehicle(updated.Id);

            // if the vehicle isn't found, return null so that we know nothing was done
            if (vehicle == null)
            {
                return null;
            }

            // if the vehicle is found, map the new values to the existing vehicle properties
            vehicle.Model = updated.Model;
            vehicle.Make = updated.Make;
            vehicle.Year = updated.Year;
            vehicle.BodyType = updated.BodyType;
            vehicle.TransmissionType = updated.TransmissionType;
            vehicle.FuelType = updated.FuelType;
            vehicle.CubicCentimeter = updated.CubicCentimeter;
            vehicle.NumberOfDoors = updated.NumberOfDoors;
            vehicle.Registration = updated.Registration;
            vehicle.Picture = updated.Picture;

            db.SaveChanges();

            // return the updated vehicle so that the caller knows an action was performed
            return vehicle;
        }

        public MOTRecord AddMOTRecord(int vehicleId, MOTRecord result)
        {
            var vehicle = this.GetVehicle(vehicleId);

            // if vehicle is not found, throw an exception to tell the user that no vehicle was found
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle {vehicleId} not found.");
            }

            // creating a new MOT record if the vehicle is found
            var newResult = new MOTRecord
            {
                Date = result.Date,
                EngineerName = result.EngineerName,
                Mileage = result.Mileage,
                Outcome = result.Outcome,
                Report = result.Report
            };

            // adding the created MOT record to the list of MOT Records
            vehicle.MOTRecords.Add(newResult);

            // finding which MOT record has the most recent date
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

            // return the new MOT record that was added
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

            // if the MOT record is not found, return null
            if (result == null)
            {
                return null;
            }

            // if the MOT Record is found, map the new values to the existing MOT record properties
            result.Date = updated.Date;
            result.EngineerName = updated.EngineerName;
            result.Mileage = updated.Mileage;
            result.Outcome = updated.Outcome;
            result.Report = updated.Report;

            db.SaveChanges();

            // return the updated result
            return result;
        }

        public bool DeleteResult(int resultId)
        {
            var r = GetMOTRecord(resultId);
            
            // if the MOT record does not exist, return null
            if (r == null)
            {
                return false;
            }

            // remove the MOT Record that was found from the db MOT collection
            db.MOTRecords.Remove(r);
            db.SaveChanges();

            //return true so that the caller knows the MOT Record was removed
            return true;
        }

        public Dictionary<string, int> GetVehicleTypeSummary()
        {
            // creating a dictionary of body types for vehicles
            var summary = new Dictionary<string, int>();

            // finding how many of each body type of vehicle has been created
            var bodyTypes = this.db.Vehicles
                .GroupBy(v => v.BodyType)
                .Select(v => new { BodyType = v.Key, Count = v.Count() });

            // adding each body type and count to the dictionary
            foreach (var bodyType in bodyTypes)
            {
                summary.Add(bodyType.BodyType.ToString(), bodyType.Count);
            }

            // returning the dictionary that has been populated
            return summary;
        }

        public Dictionary<string, int> GetMOTSeveritySummary()
        {
            // creating a dictionary to hold the number of MOT results per level of severity
            var summary = new Dictionary<string, int>();

            // finding how many of each severity type has been created
            var outcomes = this.db.MOTRecords
                .GroupBy(v => v.Outcome)
                .Select(v => new { Outcome = v.Key, Count = v.Count() });

            // adding each mot severity type and count to the dictionary
            foreach (var outcome in outcomes)
            {
                summary.Add(outcome.Outcome.ToString(), outcome.Count);
            }

            // returning the dictionary that has been populated
            return summary;
        }

        public IList<Vehicle> GetVehiclesWithUpcomingTest()
        {
            // using a where clause to find any vehicles with mot due in the next 3 months
            var vehicles = this.db.Vehicles.Where(v => v.NextMOTDate < DateTime.UtcNow.AddMonths(3));

            return vehicles.ToList();
        }
    }
}
