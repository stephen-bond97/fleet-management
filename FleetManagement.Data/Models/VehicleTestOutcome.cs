using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Models
{
    public enum VehicleTestOutcome 
    {
        Pass,
        Advisory,

        [Display(Name = "Minor Defect")]
        MinorDefect,

        [Display(Name = "Major Defect")]
        MajorDefect,

        [Display(Name = "Dangerous Defect")]
        DangerousDefect
    }
}
