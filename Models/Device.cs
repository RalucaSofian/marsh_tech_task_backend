using System.ComponentModel.DataAnnotations;

namespace DeviceManagement.Models;

public class Device
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Manufacturer { get; set; } = default!;
    public DeviceType Type { get; set; }
    public string OperatingSystem { get; set; } = default!;

    public string? OSVersion { get; set; }

    [Required]
    public string Processor { get; set; } = default!;
    public int RAM { get; set; } = default!;

    public int? UserId { get; set; }
    public string? Description { get; set; }

}

public enum DeviceType { Default, Laptop, Phone, Tablet }
