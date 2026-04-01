using DeviceManagement.Models;


namespace DeviceManagement.DTOs;

public class CreateUserInputDTO
{
    public string Name { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Location { get; set; } = default!;
}

public class UserOutputDTO
{
    public int Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Role { get; private set; } = default!;
    public string Location { get; private set; } = default!;

    public static UserOutputDTO FromDbUser(User user)
    {
        return new UserOutputDTO()
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role.ToString(),
            Location = user.Location
        };
    }
}
