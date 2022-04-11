using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Data.Models
{
    public class MOTResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public VehicleTestOutcome Outcome { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public string Report { get; set; }

        [Required]
        public string EngineerName { get; set; }
    }
}
