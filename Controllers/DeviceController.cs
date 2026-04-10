using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using DeviceManagement.Services;
using DeviceManagement.DTOs;
using DeviceManagement.Models;
using DeviceManagement.Utilities;


namespace DeviceManagement.Controllers;

[ApiController]
[EnableCors("default")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("/devices")]
public class DeviceController : ControllerBase
{
    private readonly DeviceService _deviceService;
    private readonly UserService _userService;

    public DeviceController(DeviceService deviceService, UserService userService)
    {
        _deviceService = deviceService;
        _userService = userService;
    }


    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<DeviceOutputDTO>>> GetDevices()
    {
        var dbDevicesList = await _deviceService.GetAllDevices();
        if (dbDevicesList == null)
        {
            return NotFound();
        }

        var outputDevices = dbDevicesList.Select(DeviceOutputDTO.FromDbDevice).ToList();
        return Ok(outputDevices);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<DeviceOutputDTO>> GetDevice([FromRoute] int id)
    {
        var device = await _deviceService.GetDevice(id);
        if (device == null)
        {
            return NotFound();
        }

        var outputDevice = DeviceOutputDTO.FromDbDevice(device);
        return Ok(outputDevice);
    }

    [HttpGet]
    [Route("{id}/ai_descr")]
    public async Task<ActionResult<string>> GetGeneratedDescription([FromRoute] int id)
    {
        var device = await _deviceService.GetDevice(id);
        if (device == null)
        {
            return NotFound();
        }

        var genaiInput = device.Name + device.Manufacturer +
            device.OperatingSystem + device.Type.ToString() +
            device.RAM.ToString() + "GB RAM" + device.Processor;

        var generatedDescription = await GenAIutils.GenDescription(genaiInput);
        return Ok(generatedDescription);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<DeviceOutputDTO>> CreateDevice(CreateDeviceInputDTO createDeviceInput)
    {
        var crtUser = await AuthUtils.GetCurrentUser(_userService, HttpContext.User);
        if (crtUser == null)
        {
            return BadRequest();
        }

        if (crtUser.Role != UserRole.ADMIN)
        {
            return BadRequest();
        }

        var newDevice = new Device
        {
            Name = createDeviceInput.Name,
            Manufacturer = createDeviceInput.Manufacturer,
            Type = Enum.Parse<DeviceType>(createDeviceInput.Type),
            OperatingSystem = createDeviceInput.OperatingSystem,
            OSVersion = createDeviceInput.OSVersion,
            Processor = createDeviceInput.Processor,
            RAM = createDeviceInput.RAM,
            UserId = createDeviceInput.UserId,
            Description = createDeviceInput.Description
        };

        var createdDevice = await _deviceService.CreateDevice(newDevice);
        if (createdDevice == null)
        {
            return BadRequest();
        }

        var outputDevice = DeviceOutputDTO.FromDbDevice(createdDevice);
        return Ok(outputDevice);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<DeviceOutputDTO>> EditDevice([FromRoute] int id)
    {
        var existingDevice = await _deviceService.GetDevice(id);
        if (existingDevice == null)
        {
            return NotFound();
        }

        var crtUser = await AuthUtils.GetCurrentUser(_userService, HttpContext.User);
        if (crtUser == null)
        {
            return BadRequest();
        }

        if (crtUser.Role == UserRole.USER)
        {
            if ((existingDevice.AssignedUser != null) &&
                (existingDevice.AssignedUser?.Id != crtUser.Id))
            {
                return BadRequest();
            }
        }

        var bodyReader = new StreamReader(Request.Body);
        var deviceJson = await bodyReader.ReadToEndAsync();

        var serializer = new JsonSerializer();
        using (var reader = new StringReader(deviceJson))
        {
            serializer.Populate(reader, existingDevice);
        }

        var editedDevice = await _deviceService.EditDevice(existingDevice);
        if (editedDevice == null)
        {
            return NotFound();
        }

        var outputDevice = DeviceOutputDTO.FromDbDevice(editedDevice);
        return Ok(outputDevice);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteDevice([FromRoute] int id)
    {
        var crtUser = await AuthUtils.GetCurrentUser(_userService, HttpContext.User);
        if (crtUser == null)
        {
            return BadRequest();
        }

        if (crtUser.Role != UserRole.ADMIN)
        {
            return BadRequest();
        }

        var existingDevice = await _deviceService.GetDevice(id);
        if (existingDevice == null)
        {
            return NotFound();
        }

        await _deviceService.DeleteDevice(id);
        return NoContent();
    }
}
