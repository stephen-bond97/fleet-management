using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Models
{
    public enum Role { Admin, Manager, Guest }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
