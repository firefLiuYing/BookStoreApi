using BookStoreApi.Models;
using BookStoreApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[Route("api/auth/[controller]")]
[ApiController]
public class AuthController(BookStoreContext  context):ControllerBase
{
    private readonly AuthService _authService=new(context);
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterRequest request)
    {
        var result = await _authService.Register(request);
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.Login(request);
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }
}