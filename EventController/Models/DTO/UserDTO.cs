using System.ComponentModel.DataAnnotations;

namespace EventController.Models.DTO
{
    public class UserDTO
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }  
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; } 
        public string Address { get; set; }
    }
}
