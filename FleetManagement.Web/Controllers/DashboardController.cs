using FleetManagement.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public DashboardController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("VehicleTypes")]
        public Dictionary<string, int> GetVehicleTypes()
        {
            return _vehicleService.GetVehicleTypeSummary();
        }

        [HttpGet("MOTSeverity")]
        public Dictionary<string, int> GetMOTSeverity()
        {
            return _vehicleService.GetMOTSeveritySummary();
        }
    }
}
