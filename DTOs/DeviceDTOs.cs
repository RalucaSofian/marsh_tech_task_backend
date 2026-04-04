using DeviceManagement.Models;


namespace DeviceManagement.DTOs;

public class CreateDeviceInputDTO
{
    public string Name { get; set; } = default!;
    public string Manufacturer { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string OperatingSystem { get; set; } = default!;
    public string? OSVersion { get; set; }
    public string Processor { get; set; } = default!;
    public int RAM { get; set; } = default!;
    public int? UserId { get; set; }
    public string? Description { get; set; }
}

public class DeviceOutputDTO
{
    public int Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Manufacturer { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string OperatingSystem { get; private set; } = default!;
    public string? OSVersion { get; private set; }
    public string Processor { get; private set; } = default!;
    public int RAM { get; private set; } = default!;
    public int? UserId { get; private set; }
    public string? Description { get; private set; }
    public UserOutputDTO? AssignedUser { get; private set; }

    public static DeviceOutputDTO FromDbDevice(Device device)
    {
        return new DeviceOutputDTO()
        {
            Id = device.Id,
            Name = device.Name,
            Manufacturer = device.Manufacturer,
            Type = device.Type.ToString(),
            OperatingSystem = device.OperatingSystem,
            OSVersion = device.OSVersion,
            Processor = device.Processor,
            RAM = device.RAM,
            UserId = device.UserId,
            Description = device.Description,
            AssignedUser = device.AssignedUser != null ? UserOutputDTO.FromDbUser(device.AssignedUser) : null
        };
    }
}
