using FleetManagement.Data.Models;
using FleetManagement.Data.Services;
using FleetManagement.Web.Models.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Web.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: VehicleController/List
        public ActionResult List()
        {
            var vehicles = _vehicleService.GetVehicles();

            var vm = new VehiclesListViewModel
            {
                Vehicles = vehicles
            };

            return View(vm);
        }

        // GET: VehicleController/AddVehicle
        public ActionResult AddVehicle()
        {
            return View(new Vehicle());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle = _vehicleService.AddVehicle(vehicle);
                Alert($"Vehicle Added Successfully", AlertType.success);

                return RedirectToAction(nameof(List));
            }

           return View(vehicle);
        }

        // GET: VehicleController/Edit/5
        public ActionResult EditVehicle(int id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(id);

                return View(vehicle);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        // POST: VehicleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /////////////////////////////////

        // GET: VehicleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }        

        // GET: VehicleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
