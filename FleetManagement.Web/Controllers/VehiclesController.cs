using FleetManagement.Data.Models;
using FleetManagement.Data.Services;
using FleetManagement.Web.Models.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Web.Controllers
{
    public class VehiclesController : BaseController
    {
        private readonly IVehicleService _vehicleService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehiclesController(IVehicleService vehicleService, IWebHostEnvironment webHostEnvironment)
        {
            _vehicleService = vehicleService;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost]
        public ActionResult List(VehiclesListViewModel vm)
        {
            var vehicles = _vehicleService.GetVehicles(registration: vm.Registration, make: vm.Make, model: vm.Model);

            vm.Vehicles = vehicles;

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddVehicle()
        {
            return View(new VehicleViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddVehicle(VehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // if no image was uploaded, add an error to the model
                // once the image is saved, this field becomes optional so the validation is done here instead of on the view model
                if (vm.UploadedImage == null)
                {
                    ModelState.AddModelError("UploadedImage", "Please select an image");
                    return View(vm);
                }                
                
                string uniqueFileName = UploadFile(vm);

                var vehicle = new Vehicle
                {
                    Make = vm.Make,
                    Model = vm.Model,
                    Registration = vm.Registration,
                    Year = vm.Year,
                    FuelType = vm.FuelType,
                    TransmissionType = vm.TransmissionType,
                    BodyType = vm.BodyType,
                    CubicCentimeter = vm.CubicCentimeter,
                    NumberOfDoors = vm.NumberOfDoors,
                    Picture = uniqueFileName
                };
                
                _vehicleService.AddVehicle(vehicle);
                Alert($"Vehicle Added Successfully", AlertType.success);

                return RedirectToAction(nameof(List));
            }

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditVehicle(int id)
        {
            try
            {
                var vehicle = _vehicleService.GetVehicle(id);

                var vm = new VehicleViewModel
                {
                    Make = vehicle.Make,
                    Registration = vehicle.Registration,
                    Model = vehicle.Model,
                    Year = vehicle.Year,
                    FuelType = vehicle.FuelType,
                    TransmissionType = vehicle.TransmissionType,
                    BodyType = vehicle.BodyType,
                    CubicCentimeter = vehicle.CubicCentimeter,
                    NumberOfDoors = vehicle.NumberOfDoors,
                };

                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditVehicle(int id, VehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var existingVehicle = _vehicleService.GetVehicle(id);
                
                // default the image path to the current picture
                var imagePath = existingVehicle.Picture;

                // if an image has been uploaded, we use it to overwrite the existing image path
                if (vm.UploadedImage != null)
                {
                    imagePath = UploadFile(vm);
                }

                var vehicle = new Vehicle
                {
                    Id = id,
                    Make = vm.Make,
                    Model = vm.Model,
                    Registration = vm.Registration,
                    Year = vm.Year,
                    FuelType = vm.FuelType,
                    TransmissionType = vm.TransmissionType,
                    BodyType = vm.BodyType,
                    CubicCentimeter = vm.CubicCentimeter,
                    NumberOfDoors = vm.NumberOfDoors,
                    Picture = imagePath
                };

                _vehicleService.UpdateVehicle(vehicle);
                Alert("Vehicle Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(vm);
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
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            _vehicleService.DeleteVehicle(id);

            Alert($"Vehicle Deleted Successfully", AlertType.info);

            return RedirectToAction(nameof(List));
        }

        #endregion

        #region MOT

        [HttpGet]
        public ActionResult AddMOTRecord(int id)
        {
            var vm = new MOTViewModel
            {
                VehicleId = id,
                MOTRecord = new MOTRecord()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMOTRecord(MOTViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newRecord = _vehicleService.AddMOTRecord(vm.VehicleId, vm.MOTRecord);
                Alert($"MOT Record Added Successfully", AlertType.success);

                return RedirectToAction(nameof(Details), vm.VehicleId);
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult EditMOTRecord(int id)
        {
            try
            {
                var record = _vehicleService.GetMOTRecord(id);
                var vm = new MOTViewModel
                {
                    MOTRecord = record
                };

                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditMOTRecord(MOTViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.UpdateMOTRecord(vm.MOTRecord);
                Alert("MOT Updated Successfully", AlertType.info);

                return RedirectToAction(nameof(List));
            }

            return View(vm);
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

        #region Private Methods

        // Found solution to uploading an image to the web directory on stack overflow:
        // https://stackoverflow.com/questions/65985338/asp-net-core-mvc-upload-picture-null-from-input
        private string UploadFile(VehicleViewModel model)
        {
            string uniqueFileName = null;

            if (model.UploadedImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadedImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        #endregion
    }
}