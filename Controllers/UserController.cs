using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using DeviceManagement.Services;
using DeviceManagement.DTOs;
using DeviceManagement.Models;


namespace DeviceManagement.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;


    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<UserOutputDTO>>> GetUsers()
    {
        var dbUsersList = await _userService.GetAllUsers();
        if (dbUsersList == null)
        {
            return NotFound();
        }

        var outputUsers = dbUsersList.Select(UserOutputDTO.FromDbUser).ToList();
        return Ok(outputUsers);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<UserOutputDTO>> GetUser([FromRoute] int id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }

        var outputUser = UserOutputDTO.FromDbUser(user);
        return Ok(outputUser);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<UserOutputDTO>> CreateUser(CreateUserInputDTO createUserInput)
    {
        var newUser = new User
        {
            Name     = createUserInput.Name,
            Role     = Enum.Parse<UserRole>(createUserInput.Role.ToUpper()),
            Location = createUserInput.Location
        };

        var createdUser = await _userService.CreateUser(newUser);
        if (createdUser == null)
        {
            return BadRequest();
        }

        var outputUser = UserOutputDTO.FromDbUser(createdUser);
        return Ok(outputUser);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<UserOutputDTO>> EditUser([FromRoute] int id)
    {
        var existingUser = await _userService.GetUser(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        var bodyReader = new StreamReader(Request.Body);
        var userJson = await bodyReader.ReadToEndAsync();

        var serializer = new JsonSerializer();
        using (var reader = new StringReader(userJson))
        {
            serializer.Populate(reader, existingUser);
        }

        var editedUser = await _userService.EditUser(existingUser);
        if (editedUser == null)
        {
            return NotFound();
        }

        var outputUser = UserOutputDTO.FromDbUser(editedUser);
        return Ok(outputUser);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        var existingUser = await _userService.GetUser(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.DeleteUser(id);
        return Ok();
    }
}
