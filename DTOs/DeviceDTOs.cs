using DeviceManagement.Models;


namespace DeviceManagement.DTOs;

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
            Description = device.Description
        };
    }
}
