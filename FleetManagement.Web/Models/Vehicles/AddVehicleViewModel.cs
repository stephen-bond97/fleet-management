using FleetManagement.Data.Models;

namespace FleetManagement.Web.Models.Vehicles
{
    public class AddVehicleViewModel
    {
        public Vehicle NewVehicle { get; set; }

        public AddVehicleViewModel()
        {
            this.NewVehicle = new Vehicle
            {
                Registration = "Test"
            };
        }
    }
}
