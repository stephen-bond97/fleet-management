using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Models
{
    public enum VehicleTestOutcome 
    {
        [Display(Name = "Pass")]
        Pass,

        [Display(Name = "Advisory")]
        Advisory,

        [Display(Name = "Minor Defect")]
        MinorDefect,

        [Display(Name = "Major Defect")]
        MajorDefect,

        [Display(Name = "Dangerous Defect")]
        DangerousDefect
    }
}
