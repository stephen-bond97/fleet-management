using FleetManagement.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Web.Models.Vehicles
{
    public class VehiclesListViewModel
    {
        public IList<Vehicle> Vehicles { get; set; }

        public string Registration { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
