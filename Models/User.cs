using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DeviceManagement.Models;

public class User : IdentityUser
{
    [Required]
    public string Name { get; set; } = default!;
    public UserRole Role { get; set; }
    public string Location { get; set; } = default!;
}

public enum UserRole { ADMIN, USER }
