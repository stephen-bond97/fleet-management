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

        #region Vehicle

        [HttpGet]
        public ActionResult List()
        {
            var vehicles = _vehicleService.GetVehicles();

            var vm = new VehiclesListViewModel
            {
                Vehicles = vehicles
            };

            return View(vm);
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(int id, Vehicle v)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.UpdateVehicle(v);
                Alert("Vehicle Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(v);
        }

        [HttpGet]
        public ActionResult Details(int id)
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

        [HttpGet]
        public ActionResult DeleteVehicle(int id)
        {
            var v = _vehicleService.GetVehicle(id);

            if (v == null)
            {
                Alert($"Vehicle {id} not found", AlertType.warning);
                return RedirectToAction(nameof(List));
            }

            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            _vehicleService.DeleteVehicle(id);

            Alert($"Vehicle Deleted Successfully", AlertType.info);

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region MOT

        [HttpGet]
        public ActionResult AddMOTRecord()
        {
            return View(new MOTRecord());
        }

        [HttpPost]
        public ActionResult AddMOTRecord(int vehicleId, MOTRecord record)
        {
            if (ModelState.IsValid)
            {
                record = _vehicleService.AddMOTRecord(vehicleId, record);
                Alert($"MOT Record Added Successfully", AlertType.success);

                return RedirectToAction(nameof(Details), vehicleId);
            }

            return View(record);
        }

        [HttpGet]
        public ActionResult EditMOTRecord(int id)
        {
            try
            {
                var record = _vehicleService.GetMOTRecord(id);

                return View(record);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMOTRecord(int id, MOTRecord m)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.UpdateMOTRecord(m);
                Alert("MOT Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(m);
        }

        [HttpGet]
        public ActionResult DeleteRecord(int id)
        {
            var r = _vehicleService.GetMOTRecord(id);

            if (r == null)
            {
                Alert($"MOT Record {id} not found", AlertType.warning);
                return RedirectToAction(nameof(List));
            }
            return View(r);
        }

        [HttpGet]
        public ActionResult DeleteMOTConfirm(int id)
        {
            _vehicleService.DeleteResult(id);

            Alert($"Record Deleted Successfully", AlertType.info);

            return RedirectToAction(nameof(List));
        }

        #endregion
    }
}
