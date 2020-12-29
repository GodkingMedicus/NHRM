using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthernHealthAPI.Models2;
using Microsoft.EntityFrameworkCore;

namespace NorthernHealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientMeasurementsRecordController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public PatientMeasurementsRecordController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/PatientMeasurementsRecord/urno
        [HttpGet("{ur}")]
        public async Task<ActionResult<IEnumerable<PatientMeasurementRecord>>> GetPatientMeasurement(string ur)
        {
            var patientMeasurement = await _context.MeasurementRecord.Join(_context.DataPointRecord,
                m => m.MeasurementRecordId,
                d => d.MeasurementRecordId,
               (m, d) => new { m, d })
                .Where(md => md.m.Urnumber == ur)
                .Select(
                md => new PatientMeasurementRecord
                {
                    DateTimeRecorded = md.m.DateTimeRecorded,
                    Value = md.d.Value,
                    MeasurementId = md.d.MeasurementId
                }
                ).ToListAsync();

            return patientMeasurement;
        }
    }
}
