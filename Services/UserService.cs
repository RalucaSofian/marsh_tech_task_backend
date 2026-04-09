using Microsoft.EntityFrameworkCore;

using DeviceManagement.Data;
using DeviceManagement.Models;


namespace DeviceManagement.Services;

public class UserService
{
    private readonly DeviceManagementDb _context;


    public UserService(DeviceManagementDb context)
    {
        _context = context;
    }


    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.OrderBy(u => u.Id).ToListAsync();
    }

    public async Task<User?> GetUser(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> CreateUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();

        var createdUser = await _context.FindAsync<User>(user.Id);
        if (createdUser == null)
        {
            return null;
        }

        return createdUser;
    }

    public async Task<User?> EditUser(User user)
    {
        try
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(user.Id))
            {
                return null;
            }
        }

        var editedUser = await _context.Users.FindAsync(user.Id);
        return editedUser;
    }

    public async Task DeleteUser(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
        await _context.SaveChangesAsync();
    }


    private bool UserExists(string id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}