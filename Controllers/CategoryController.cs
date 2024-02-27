using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>("05XE04 Falha interna no servidor"));
            }
        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            try
            {
                Category? category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Categoria não encontrada"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE05 Falha interna no servidor"));
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromServices] BlogDataContext context,
            [FromBody] EditorCategoryViewModel model)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest(new ResultViewModel<Category>(
            //        ModelState.Values
            //            .SelectMany(x => x.Errors)
            //            .Select(x => x.ErrorMessage)
            //            .ToList())
            //    );

            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            // o ASP.NET ja realiza essa validacao por padrao, mas caso seja necessario manipular uma validacao eh possivel usar o ModelState


            try
            {
                Category category = new Category
                {
                    Id = 0,
                    Posts = [],
                    Name = model.Name,
                    Slug = model.Slug
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException e)
            {
                // return BadRequest("Não foi inculir a categoria"); // qnd da requisicao invalida
                return StatusCode(500, new ResultViewModel<Category>("05XE9 - Não foi possível inculir a categoria"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("05XE10 Falha interna no servidor"));
            }
        }

        // Deixei sem o retorno padrao nas actions abaixo

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            try
            {
                Category? category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE8 - Não foi possível alterar a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE11 Falha interna no servidor");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            try
            {
                Category? category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, "05XE7 - Não foi possível deletar a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE12 Falha interna no servidor");
            }
        }
    }
}
