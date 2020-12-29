using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthernHealthAPI.Models2;

namespace NorthernHealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateCategoriesController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public TemplateCategoriesController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/TemplateCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateCategory>>> GetTemplateCategory()
        {
            return await _context.TemplateCategory.ToListAsync();
        }

        // GET: api/TemplateCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateCategory>> GetTemplateCategory(int id)
        {
            var templateCategory = await _context.TemplateCategory.FindAsync(id);

            if (templateCategory == null)
            {
                return NotFound();
            }

            return templateCategory;
        }

        // PUT: api/TemplateCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplateCategory(int id, TemplateCategory templateCategory)
        {
            if (id != templateCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(templateCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TemplateCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TemplateCategory>> PostTemplateCategory(TemplateCategory templateCategory)
        {
            _context.TemplateCategory.Add(templateCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemplateCategory", new { id = templateCategory.CategoryId }, templateCategory);
        }

        // DELETE: api/TemplateCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TemplateCategory>> DeleteTemplateCategory(int id)
        {
            var templateCategory = await _context.TemplateCategory.FindAsync(id);
            if (templateCategory == null)
            {
                return NotFound();
            }

            _context.TemplateCategory.Remove(templateCategory);
            await _context.SaveChangesAsync();

            return templateCategory;
        }

        private bool TemplateCategoryExists(int id)
        {
            return _context.TemplateCategory.Any(e => e.CategoryId == id);
        }
    }
}
