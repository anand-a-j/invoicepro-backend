

using InvoicePro.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly ICurrentUser _c;

    public TestController(ICurrentUser c)
    {
        _c = c;
    }


    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = _c.UserId,
            Email = _c.Email,
            Role = _c.Role
        });
    }
}