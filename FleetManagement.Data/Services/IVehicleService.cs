using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IVehicleService
    {
        Vehicle AddVehicle(Vehicle vehicle);
        IList<Vehicle> GetVehicles();
        void Initialise();
    }
}