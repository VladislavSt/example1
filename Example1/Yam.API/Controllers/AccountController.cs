using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yam.API.Model.Dtos.Account;
using Yam.API.Model.Dtos.Account.Request;
using Yam.API.Model.Dtos.Account.Response;
using Yam.API.Services;
using Yam.ApplicationServices.Services;
using Yam.Model.Models;

namespace Yam.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    // Identity package
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly INotificationService _notificationService;

    public AccountController(UserManager<IdentityUser> userManager, IJwtService jwtService, INotificationService notificationService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _notificationService = notificationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO user)
    {
        if (ModelState.IsValid)
        {
            IdentityUser existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = new List<string>() { "Email already Registered" },
                    Success = false
                });
            }

            IdentityUser newUser = new IdentityUser()
            {
                Email = user.Email,
                UserName = user.Username,
            };

            IdentityResult? created = await _userManager.CreateAsync(newUser, user.Password);
            if (created.Succeeded)
            {
                _notificationService.Send(new Notification($"User with email:{user.Email} registred."));

                AuthResult authResult = await _jwtService.GenerateToken(newUser);
                //return a token
                return Ok(authResult);
            }
            else
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = created.Errors.Select(e => e.Description).ToList(),
                    Success = false
                });
            }
        }

        return BadRequest(new RegisterResponseDTO()
        {
            Errors = new List<string>() { "Invalid payload" },
            Success = false
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> login(LoginUserDTO user)
    {
        if (ModelState.IsValid)
        {
            IdentityUser existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = new List<string>() { "Email address is not registered." },
                    Success = false
                });
            }

            bool isUserCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (isUserCorrect)
            {
                _notificationService.Send(new Notification($"User with email:{user.Email} logined."));

                AuthResult authResult = await _jwtService.GenerateToken(existingUser);
                //return a token
                return Ok(authResult);
            }
            else
            {
                return BadRequest(new RegisterResponseDTO()
                {
                    Errors = new List<string>() { "Wrong password" },
                    Success = false
                });
            }
        }

        return BadRequest(new RegisterResponseDTO()
        {
            Errors = new List<string>() { "Invalid payload" },
            Success = false
        });
    }

    [HttpPost("refreshtoken")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequest)
    {
        if (ModelState.IsValid)
        {
            var verified = await _jwtService.VerifyToken(tokenRequest);
            //
            if (!verified.Success)
            {
                return BadRequest(new AuthResult()
                {
                    // Errors = new List<string> { "invalid Token" },
                    Errors = verified.Errors,
                    Success = false
                });
            }

            var tokenUser = await _userManager.FindByIdAsync(verified.UserId);
            AuthResult authResult = await _jwtService.GenerateToken(tokenUser);

            //return a token
            return Ok(authResult);
        }

        return BadRequest(new AuthResult()
        {
            Errors = new List<string> { "invalid Payload" },
            Success = false
        });
    }

    [HttpGet("info")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetInfo()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user != null)
        {
            var userInfo = new UserResponseDTO()
            {
                Email = user.Email,
                Username = user.UserName
            };

            return Ok(userInfo);
        }

        return NotFound();
    }
}
