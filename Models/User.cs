using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    public string Name {get; set;} = default!;
    public UserRole Role { get; set; }
    public string Location { get; set; } = default!;
}

public enum UserRole { ADMIN, USER }
