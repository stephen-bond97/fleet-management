using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IVehicleService
    {
        Vehicle AddVehicle(Vehicle vehicle);

        bool DeleteVehicle(int id);

        Vehicle UpdateVehicle(Vehicle vehicle);

        MOTRecord AddMOTRecord(int vehicleId, MOTRecord result);

        MOTRecord GetMOTRecord(int vehicleId);

        MOTRecord UpdateMOTRecord(MOTRecord result);

        bool DeleteResult(int resultId);

        Vehicle GetVehicle(int id);

        IList<Vehicle> GetVehicles();

        Dictionary<string, int> GetVehicleTypeSummary();

        Dictionary<string, int> GetMOTSeveritySummary();
        IList<Vehicle> GetVehiclesWithUpcomingTest();
    }
}