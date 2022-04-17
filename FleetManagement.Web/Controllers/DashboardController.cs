using FleetManagement.Data.Repository;
using FleetManagement.Data.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<DashboardController>
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
