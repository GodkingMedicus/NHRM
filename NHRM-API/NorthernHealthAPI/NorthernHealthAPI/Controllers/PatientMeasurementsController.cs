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
    public class PatientMeasurementsController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public PatientMeasurementsController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/PatientMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientMeasurement>>> GetPatientMeasurement()
        {
            return await _context.PatientMeasurement.ToListAsync();
        }

        // GET: api/PatientMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientMeasurement>> GetPatientMeasurement(int id)
        {
            var patientMeasurement = await _context.PatientMeasurement.FindAsync(id);

            if (patientMeasurement == null)
            {
                return NotFound();
            }

            return patientMeasurement;
        }

        // PUT: api/PatientMeasurements/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientMeasurement(int id, PatientMeasurement patientMeasurement)
        {
            if (id != patientMeasurement.MeasurementId)
            {
                return BadRequest();
            }

            _context.Entry(patientMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientMeasurementExists(id))
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

        // POST: api/PatientMeasurements
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PatientMeasurement>> PostPatientMeasurement(PatientMeasurement patientMeasurement)
        {
            _context.PatientMeasurement.Add(patientMeasurement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientMeasurementExists(patientMeasurement.MeasurementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatientMeasurement", new { id = patientMeasurement.MeasurementId }, patientMeasurement);
        }

        // DELETE: api/PatientMeasurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientMeasurement>> DeletePatientMeasurement(int id)
        {
            var patientMeasurement = await _context.PatientMeasurement.FindAsync(id);
            if (patientMeasurement == null)
            {
                return NotFound();
            }

            _context.PatientMeasurement.Remove(patientMeasurement);
            await _context.SaveChangesAsync();

            return patientMeasurement;
        }

        private bool PatientMeasurementExists(int id)
        {
            return _context.PatientMeasurement.Any(e => e.MeasurementId == id);
        }
    }
}
