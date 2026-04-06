using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public UserRole Role { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Location { get; set; } = default!;
}

public enum UserRole { ADMIN, USER }
