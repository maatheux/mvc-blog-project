using Blog.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        //[ApiKey]
        public IActionResult Get(
            [FromServices] IConfiguration config)
        {
            return Ok(new
            {
                enviroment = config.GetValue<string>("Env")
            });
        } // HEALTH CHECK --> outras apis/requests conseguem checar a saude (status) dessa API
    }
}
