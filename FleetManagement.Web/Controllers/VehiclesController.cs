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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(int id, [Bind("Id, Make, Model, Registration, Year, FuelType, TransmissionType, BodyType, CubicCentimeter, NumberOfDoors")] Vehicle v)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.UpdateVehicle(v);
                Alert("Vehicle Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(v);
        }

        // GET: VehicleController/Details/5
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

        ///////////////////////////////////

        // POST: VehicleController/Delete/5
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

        public ActionResult EditMOTRecord(int id)
        {
            try
            {
                var record = _vehicleService.GetMOTResult(id);

                return View(record);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMOTRecord(int id, [Bind("Id, Date, Outcome, Mileage, Report, EngineerName")] MOTResult m)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.UpdateMOTResult(m);
                Alert("MOT Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(m);
        }

        #endregion
    }
}
