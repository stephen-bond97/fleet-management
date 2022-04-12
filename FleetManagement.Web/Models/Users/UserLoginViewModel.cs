using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Web.Models.Users
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
