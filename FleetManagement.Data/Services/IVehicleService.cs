using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IVehicleService
    {
        Vehicle AddVehicle(Vehicle vehicle);

        Vehicle UpdateVehicle(Vehicle vehicle);

        MOTResult AddMOTResult(int vehicleId, MOTResult result);

        MOTResult GetMOTResult(int vehicleId);

        MOTResult UpdateMOTResult(MOTResult result);

        Vehicle GetVehicle(int id);

        IList<Vehicle> GetVehicles();

        void Initialise();
    }
}