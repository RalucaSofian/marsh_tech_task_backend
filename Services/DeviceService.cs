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


    public async Task<List<Device>> SearchDevices(string searchString)
    {
        var result = await _context.Devices.FromSql($"""
            SELECT outerDevice.*
            FROM (SELECT [key], Sum([rank]) AS WeightedRank
                    FROM (
                    SELECT [key], [rank] * 50 as [rank]
                    FROM Freetexttable(dbo.Devices, [Name], {searchString})
                    UNION ALL

                    SELECT [key], [rank] * 20 as [rank]
                    FROM Freetexttable(dbo.Devices, [Manufacturer], {searchString})
                    UNION ALL

                    SELECT Id as [key], 15 as [rank]
                    FROM Devices WHERE RAM = TRY_PARSE({searchString} AS int)
                    UNION ALL

                    SELECT [key], [rank] * 15 as [rank]
                    FROM Freetexttable(dbo.Devices, [Processor], {searchString})
                    )innerSearch
            GROUP BY [key]) ranksGroupedByDeviceID
            INNER JOIN dbo.Devices outerDevice ON outerDevice.Id = ranksGroupedByDeviceID.[key]
            ORDER BY WeightedRank DESC;
        """).ToListAsync();


        return result;

    }

    public async Task<List<Device>> GetAllDevices()
    {
        return await _context.Devices.OrderBy(d => d.Id).Include(d => d.AssignedUser).ToListAsync();
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