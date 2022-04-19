using FleetManagement.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Web.Models.Vehicles
{
    public class VehicleViewModel
    {
        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Registration { get; set; }

        [Range(1904, 2022)]
        public int Year { get; set; } = DateTime.UtcNow.Year;

        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Transmission Type")]
        public TransmissionType TransmissionType { get; set; }

        [Display(Name = "Body Type")]
        public BodyType BodyType { get; set; }

        [Display(Name = "CCs")]
        [Range(600, 10000)]
        public int CubicCentimeter { get; set; }

        public IFormFile UploadedImage { get; set; }

        [Display(Name = "Number of Doors")]
        [Range(0, 10)]
        public int NumberOfDoors { get; set; }
    }
}
