using InvoicePro.API.Core;
using InvoicePro.Application.DTOs.Auth;
using InvoicePro.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicePro.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto req)
    {
        var result = await _authService.RegisterAsync(req);
        return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Registered successfully"));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto req)
    {
        var result = await _authService.LoginAsync(req);
        return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Login successfully"));
    }
}