using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")] // versionamento
        public IActionResult Get(
            [FromServices] BlogDataContext context)
        {
            IList<Category> categories = context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("v2/categories")]
        public IActionResult Get2(
            [FromServices] BlogDataContext context)
        {
            IList<Category> categories = context.Categories.ToList();
            return Ok(categories);
        }
    }
}
