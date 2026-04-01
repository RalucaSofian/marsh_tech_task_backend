using Microsoft.EntityFrameworkCore;

using DeviceManagement.Data;
using DeviceManagement.Models;


namespace DeviceManagement.Services;

public class DeviceService
{
    private readonly DeviceManagementDb _context;


    public DeviceService(DeviceManagementDb context)
    {
        _context = context;
    }


    public async Task<List<Device>> GetAllDevices()
    {
        return await _context.Devices.OrderBy(d => d.Id).ToListAsync();
    }

    public async Task<Device?> GetDevice(int id)
    {
        var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);
        if (device == null)
        {
            return null;
        }

        return device;
    }

    public async Task<Device?> CreateDevice(Device device)
    {
        _context.Add(device);
        await _context.SaveChangesAsync();

        var createdDevice = await _context.FindAsync<Device>(device.Id);
        if (createdDevice == null)
        {
            return null;
        }

        return createdDevice;
    }

    public async Task<Device?> EditDevice(Device device)
    {
        try
        {
            _context.Update(device);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DeviceExists(device.Id))
            {
                return null;
            }
        }

        var editedDevice = await _context.Devices.FindAsync(device.Id);
        return editedDevice;
    }

    public async Task DeleteDevice(int id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device != null)
        {
            _context.Devices.Remove(device);
        }
        await _context.SaveChangesAsync();
    }


    private bool DeviceExists(int id)
    {
        return _context.Devices.Any(e => e.Id == id);
    }
}