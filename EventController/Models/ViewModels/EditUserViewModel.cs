namespace EventController.Models.ViewModels;

using System.ComponentModel.DataAnnotations;

public class EditUserViewModel
{
    public int UserID { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; }

    [EmailAddress]
    public string Email { get; set; } 

    [MaxLength(11)]
    [RegularExpression(@"^\d{1,11}$", ErrorMessage = "Phone must contain only digits and be up to 11 characters.")]
    public string Phone { get; set; }

    public string Gender { get; set; }

    public DateTime DoB { get; set; }

    public string Address { get; set; }

    public IFormFile? ProfileImageFile { get; set; }

    public string? ProfileImage { get; set; } 
}
