using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

using DeviceManagement.Models;
using DeviceManagement.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace DeviceManagement.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<string>> Login(LoginInputDTO loginInput)
    {
        var user = await _userManager.FindByEmailAsync(loginInput.Email);
        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginInput.Password, true);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        var jwtSecToken = CreateJWT(user);
        if (jwtSecToken == string.Empty)
        {
            return Unauthorized();
        }

        return jwtSecToken;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<ActionResult<string>> Signup(RegisterInputDTO signupInput)
    {
        if (signupInput.Password != signupInput.ConfirmPassword)
        {
            return BadRequest();
        }

        var newUser = new User
        {
            Name = signupInput.Name,
            Email = signupInput.Email,
            UserName = signupInput.Email,
            Role = UserRole.USER,
            Location = signupInput.Location,
        };
        var result = await _userManager.CreateAsync(newUser, signupInput.Password);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        var createdUser = await _userManager.FindByEmailAsync(newUser.Email);
        if (createdUser == null)
        {
            return BadRequest();
        }

        var jwtSecToken = CreateJWT(newUser);
        if (jwtSecToken == string.Empty)
        {
            return BadRequest();
        }

        return jwtSecToken;
    }


    private string CreateJWT(User userInfo)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var envSecretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
        if (envSecretKey == null)
        {
            return string.Empty;
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(envSecretKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var jwtOptions = new JwtSecurityToken(
            issuer: "http://localhost:5047",
            audience: "http://localhost:5047",
            claims: claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: signingCredentials
        );

        var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtOptions);
        return jwtString;
    }
}