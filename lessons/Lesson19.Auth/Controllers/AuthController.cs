using System.Security.Claims;
using Lesson14.Models;
using Lesson14.Models.Requests;
using Lesson14.Models.Responses;
using Lesson19.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson19.Auth.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest model)
    {
        var (acc, token) = await _authService.Register(model.Name, model.Email, model.Password);
        return new RegisterResponse { Account = acc, Token = token };
    }

    [HttpPost("login")]
    public async Task<ActionResult<LogInResponse>> LogIn(LogInRequest model)
    {
        try
        {
            var (acc, token) = await _authService.LogIn(model.Email, model.Password);
            return new LogInResponse { Account = acc, Token = token };
        }
        catch (EmailNotFoundException)
        {
            return new LogInResponse("Email is not found");
        }
        catch (IncorrectPasswordException)
        {
            return new LogInResponse("Incorrect password");
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult LogOut()
    {
        return Ok();
    }

    [Authorize]
    [HttpGet("get_account")]
    public Task<Account> GetCurrentAccount()
    {
        var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var guid = Guid.Parse(strId);

        return Task.FromResult(Account.Fake with { Id = guid });
    }

    [Authorize(Roles = $"{Role.Admin},{Role.Buyer}")]
    [HttpGet("admin")]
    public IActionResult GetAllAccounts()
    {
        return Ok();
    }
}