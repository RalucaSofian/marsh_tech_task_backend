using System.Security.Claims;
using DeviceManagement.Models;
using DeviceManagement.Services;


namespace DeviceManagement.Utilities;

public class AuthUtils
{
    public static async Task<User?> GetCurrentUser(UserService userService, ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return null;
        }

        var user = await userService.GetUser(userId);
        return user;
    }
}
