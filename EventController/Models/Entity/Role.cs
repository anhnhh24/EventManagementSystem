using EventController.Models.Entity;
using System.ComponentModel.DataAnnotations;

public class Role
{
    [Key]
    public int RoleID { get; set; }

    [Required, MaxLength(50)]
    public string RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; }
}