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
    public class PatientCategoriesController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public PatientCategoriesController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/PatientCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCategory>>> GetPatientCategory()
        {
            return await _context.PatientCategory.ToListAsync();
        }

        // GET: api/PatientCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientCategory>> GetPatientCategory(int id)
        {
            var patientCategory = await _context.PatientCategory.FindAsync(id);

            if (patientCategory == null)
            {
                return NotFound();
            }

            return patientCategory;
        }

        // PUT: api/PatientCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientCategory(int id, PatientCategory patientCategory)
        {
            if (id != patientCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(patientCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientCategoryExists(id))
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

        // POST: api/PatientCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PatientCategory>> PostPatientCategory(PatientCategory patientCategory)
        {
            _context.PatientCategory.Add(patientCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientCategoryExists(patientCategory.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatientCategory", new { id = patientCategory.CategoryId }, patientCategory);
        }

        // DELETE: api/PatientCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientCategory>> DeletePatientCategory(int id)
        {
            var patientCategory = await _context.PatientCategory.FindAsync(id);
            if (patientCategory == null)
            {
                return NotFound();
            }

            _context.PatientCategory.Remove(patientCategory);
            await _context.SaveChangesAsync();

            return patientCategory;
        }

        private bool PatientCategoryExists(int id)
        {
            return _context.PatientCategory.Any(e => e.CategoryId == id);
        }
    }
}
