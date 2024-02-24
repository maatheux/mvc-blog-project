﻿using Blog.Data;
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
                IList<Category> categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE04 Falha interna no servidor");
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
                    return NotFound();

                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE05 Falha interna no servidor");
            }
        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromServices] BlogDataContext context,
            [FromBody] EditorCategoryViewModel model)
        {

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

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (DbUpdateException e)
            {
                // return BadRequest("Não foi inculir a categoria"); // qnd da requisicao invalida
                return StatusCode(500, "05XE9 - Não foi possível inculir a categoria");
            }
            catch (Exception e)
            {
                return StatusCode(500, "05XE10 Falha interna no servidor");
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model)
        {
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
