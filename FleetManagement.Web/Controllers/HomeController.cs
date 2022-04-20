using FleetManagement.Data.Services;
using FleetManagement.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IVehicleService _vehicleService;

        public HomeController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public ActionResult Index()
        {
            var vm = new HomeViewModel();

            var vehicles = _vehicleService.GetVehiclesWithUpcomingTest();

            vm.VehiclesDueMOT = vehicles;

            return View(vm);
        }
    }
}
