using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Data.Models
{
    public enum Role { Admin, Manager, Guest }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
