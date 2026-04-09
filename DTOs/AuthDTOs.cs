namespace DeviceManagement.DTOs;

public class LoginInputDTO
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class RegisterInputDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string Location { get; set; } = default!;
}