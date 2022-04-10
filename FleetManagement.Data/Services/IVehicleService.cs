using FleetManagement.Data.Models;

namespace FleetManagement.Data.Services
{
    public interface IVehicleService
    {
        Vehicle AddVehicle(Vehicle vehicle);

        Vehicle GetVehicle(int id);

        IList<Vehicle> GetVehicles();

        void Initialise();
    }
}