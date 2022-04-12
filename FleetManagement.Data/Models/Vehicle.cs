using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Registration { get; set; }

        [Range(1904, 2022)]
        public int Year { get; set; } = DateTime.Now.Year;

        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [Display(Name = "Transmission Type")]
        public TransmissionType TransmissionType { get; set; }

        [Display(Name = "Body Type")]
        public BodyType BodyType { get; set; }

        [Display(Name = "CCs")]
        [Range(600, 10000)]
        public int CubicCentimeter { get; set; }

        [Display(Name = "Number of Doors")]
        [Range(0, 10)]
        public int NumberOfDoors { get; set; }

        public List<MOTRecord> MOTRecords { get; set; }
    }
}
