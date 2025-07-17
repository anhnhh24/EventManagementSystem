
using System.ComponentModel.DataAnnotations;

namespace EventController.Models.ViewModels
{
    public class UserViewModel
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6,
        ErrorMessage = "Password must be 6-15 characters long.")]
        [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$",
        ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public int RoleID { get; set; }

        [Required]
        [MaxLength(11)]
        [RegularExpression(@"^\d{1,11}$", ErrorMessage = "Phone must contain only digits and be up to 11 characters.")]
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; } 
        public string Address { get; set; }
        public string? ProfileImage { get; set; }
    }
}
