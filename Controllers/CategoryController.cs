using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        [HttpGet("categorias")] /* dois endpoints para uma mesma action */
        public IActionResult Get(
            [FromServices] BlogDataContext context)
        {
            IList<Category> categories = context.Categories.ToList();
            return Ok(categories);
        }
    }
}
