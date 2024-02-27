using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    // private readonly TokenService _tokenService;
    //
    // public AccountController(TokenService tokenService)
    // {
    //     _tokenService = tokenService;
    // }
    // Um pouco de injecao de dependecia
    // Porem esse trecho de codigo eh equivalente ao o q o ASP.NET faz com o [FromServices]
    
    [HttpPost("v1/login")]
    public IActionResult Login([FromServices] TokenService tokenService)
    {
        var token = tokenService.GenerateToken(null);
        
        return Ok(token);
    }
}