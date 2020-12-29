using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NorthernHealthAPI.Models2;

namespace NorthernHealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public PatientsController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            return await _context.Patient.ToListAsync();
        }

        //Returns a SearchPatient object (Patient object without Salt & Password)
        [HttpPost, Route("search")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient(SearchPatient pt)
        {
            List<Patient> patients = new List<Patient>();

            if (pt.Urnumber != "")
            {
                Console.WriteLine("HospitalSearch");
                //patients = await _context.Patient.Where(x => x.Urnumber == pt.Urnumber).ToListAsync();
                patients = await (from s in _context.Patient
                                  where s.Urnumber == pt.Urnumber
                                  select new Patient
                                  {
                                      Urnumber = s.Urnumber,
                                      Email = s.Email,
                                      Title = s.Title,
                                      FirstName = s.FirstName,
                                      SurName = s.SurName,
                                      Gender = s.Gender,
                                      Dob = s.Dob,
                                      Address = s.Address,
                                      Suburb = s.Suburb,
                                      PostCode = s.PostCode,
                                      MobileNumber = s.MobileNumber,
                                      HomeNumber = s.HomeNumber,
                                      CountryOfBirth = s.CountryOfBirth,
                                      PreferredLanguage = s.PreferredLanguage,
                                      LivesAlone = s.LivesAlone,
                                      Active = s.Active,
                                      Password = null,
                                      Salt = null,
                                      RegisteredBy = s.RegisteredBy
                                  }).ToListAsync();
            }
            else if (pt.FirstName != "" && pt.SurName != "")
            {
                Console.WriteLine("F+S Search");
                //patients = await _context.Patient.Where(x => x.FirstName == pt.FirstName && x.SurName == pt.SurName).ToListAsync();
                patients = await (from s in _context.Patient
                                  where s.FirstName == pt.FirstName && s.SurName == pt.SurName
                                  select new Patient
                                  {
                                      Urnumber = s.Urnumber,
                                      Email = s.Email,
                                      Title = s.Title,
                                      FirstName = s.FirstName,
                                      SurName = s.SurName,
                                      Gender = s.Gender,
                                      Dob = s.Dob,
                                      Address = s.Address,
                                      Suburb = s.Suburb,
                                      PostCode = s.PostCode,
                                      MobileNumber = s.MobileNumber,
                                      HomeNumber = s.HomeNumber,
                                      CountryOfBirth = s.CountryOfBirth,
                                      PreferredLanguage = s.PreferredLanguage,
                                      LivesAlone = s.LivesAlone,
                                      Active = s.Active,
                                      Password = null,
                                      Salt = null
                                  }).ToListAsync();
            }
            else if (pt.SurName != "")
            {
                Console.WriteLine("S Search");
                //patients = await _context.Patient.Where(x => x.SurName == pt.SurName).ToListAsync();
                patients = await (from s in _context.Patient
                                  where s.SurName == pt.SurName
                                  select new Patient
                                  {
                                      Urnumber = s.Urnumber,
                                      Email = s.Email,
                                      Title = s.Title,
                                      FirstName = s.FirstName,
                                      SurName = s.SurName,
                                      Gender = s.Gender,
                                      Dob = s.Dob,
                                      Address = s.Address,
                                      Suburb = s.Suburb,
                                      PostCode = s.PostCode,
                                      MobileNumber = s.MobileNumber,
                                      HomeNumber = s.HomeNumber,
                                      CountryOfBirth = s.CountryOfBirth,
                                      PreferredLanguage = s.PreferredLanguage,
                                      LivesAlone = s.LivesAlone,
                                      Active = s.Active,
                                      Password = null,
                                      Salt = null
                                  }).ToListAsync();
            }
            else if (pt.FirstName != "")
            {
                Console.WriteLine("F Search");
                //patients = await _context.Patient.Where(x => x.FirstName == pt.FirstName).ToListAsync();
                patients = await (from s in _context.Patient
                                  where s.FirstName == pt.FirstName
                                  select new Patient
                                  {
                                      Urnumber = s.Urnumber,
                                      Email = s.Email,
                                      Title = s.Title,
                                      FirstName = s.FirstName,
                                      SurName = s.SurName,
                                      Gender = s.Gender,
                                      Dob = s.Dob,
                                      Address = s.Address,
                                      Suburb = s.Suburb,
                                      PostCode = s.PostCode,
                                      MobileNumber = s.MobileNumber,
                                      HomeNumber = s.HomeNumber,
                                      CountryOfBirth = s.CountryOfBirth,
                                      PreferredLanguage = s.PreferredLanguage,
                                      LivesAlone = s.LivesAlone,
                                      Active = s.Active,
                                      Password = null,
                                      Salt = null
                                  }).ToListAsync();
            }



            if (patients == null || patients.Count < 1)
            {
                Console.WriteLine("NotFound");
                return NotFound();
            }

            return patients;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(string id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(string id, Patient patient)
        {
            /*if (id != patient.Urnumber)
            {
                return BadRequest();
            }*/

            //_context.Entry(patient).State = EntityState.Modified;

            string query = "UPDATE patient SET " +
                //"Urnumber=@Urnumber, " +
                "Email=@Email, " +
                "Title=@Title, " +
                "FirstName=@FirstName, " +
                "Surname=@Surname, " +
                "Gender=@Gender, " +
                "Dob=@Dob, " +
                "Address=@Address, " +
                "Suburb=@Suburb, " +
                "PostCode=@PostCode, " +
                "MobileNumber=@MobileNumber, " +
                "HomeNumber=@HomeNumber, " +
                "CountryOfBirth=@CountryOfBirth, " +
                "PreferredLanguage=@PreferredLanguage, " +
                "LivesAlone=@LivesAlone, " +
                "Active=@Active WHERE Urnumber=@id";

            _context.Database.ExecuteSqlRaw(query,
                new SqlParameter("@Email", patient.Email),
                new SqlParameter("@Title", patient.Title),
                new SqlParameter("@FirstName", patient.FirstName),
                new SqlParameter("@Surname", patient.SurName),
                new SqlParameter("@Gender", patient.Gender),
                new SqlParameter("@Dob", patient.Dob),
                new SqlParameter("@Address", patient.Address),
                new SqlParameter("@Suburb", patient.Suburb),
                new SqlParameter("@PostCode", patient.PostCode),
                new SqlParameter("@MobileNumber", patient.MobileNumber),
                new SqlParameter("@HomeNumber", patient.HomeNumber),
                new SqlParameter("@CountryOfBirth", patient.CountryOfBirth),
                new SqlParameter("@PreferredLanguage", patient.PreferredLanguage),
                new SqlParameter("@LivesAlone", patient.LivesAlone),
                new SqlParameter("@Active", patient.Active),
                new SqlParameter("@id", id)
                );



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        [HttpPut, Route("Password")]
        public async Task<IActionResult> PutPatientPassword(Patient patient)
        {
            Console.WriteLine("hit1");
            GenerateSaltAndPassword(patient);

            
            string query = "UPDATE patient SET " +
                "Password=@Password, " +
                "Salt=@Salt WHERE URnumber=@id";

            _context.Database.ExecuteSqlRaw(query,
                new SqlParameter("@Password", patient.Password),
                new SqlParameter("@Salt", patient.Salt),
                new SqlParameter("@id", patient.Urnumber)
                );

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(patient.Urnumber))
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

        // POST: api/Patients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            patient = GenerateSaltAndPassword(patient);

            _context.Patient.Add(patient);

            PatientCategory pC = new PatientCategory();
            PatientMeasurement pM = new PatientMeasurement();

            //Attaches an IPC category to a patient
            pC.Urnumber = patient.Urnumber;
            pC.CategoryId = 1;

            _context.PatientCategory.Add(pC);

            //Attaches 5 measurements to the PatientCategory
            pM.CategoryId = 1;
            pM.Urnumber = patient.Urnumber;
            pM.MeasurementId = 5;
            pM.Frequency = 7;
            pM.FrequencySetDate = DateTime.Now;
            pM.PatientCategory = pC;
            pM.Measurement = await _context.Measurement.FindAsync(pM.MeasurementId);

            try
            {
                _context.PatientMeasurement.Add(pM);
            }
            catch (DbUpdateException)
            {
                if (_context.PatientMeasurement.Any(e => e.Urnumber == patient.Urnumber && e.MeasurementId == pM.MeasurementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            pM.Frequency = 1;

            for (int i = 1; i < 5; i++)
            {
                PatientMeasurement pM2 = new PatientMeasurement();

                pM2.CategoryId = 1;
                pM2.Urnumber = patient.Urnumber;
                pM2.Frequency = 1;
                pM2.FrequencySetDate = DateTime.Now;
                pM2.PatientCategory = pC;
                pM2.MeasurementId = i;
                pM2.Measurement = await _context.Measurement.FindAsync(pM2.MeasurementId);

                try
                {
                    _context.PatientMeasurement.Add(pM2);
                }
                catch (DbUpdateException)
                {
                    if (_context.PatientMeasurement.Any(e => e.Urnumber == patient.Urnumber && e.MeasurementId == pM2.MeasurementId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //Attaches PatientResources to PatientCategory
            for (int i = 1; i < 4; i++)
            {
                PatientResource pR = new PatientResource();
                pR.CategoryId = 1;
                pR.Urnumber = patient.Urnumber;
                pR.ResourceId = i;
                _context.PatientResource.Add(pR);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.Urnumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = patient.Urnumber }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(string id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(string id)
        {
            return _context.Patient.Any(e => e.Urnumber == id);
        }

        private Patient GenerateSaltAndPassword(Patient patient)
        {
            //Password needs to be randomly generated. Currently preset for testing purposes
            //patient.Password = Encoding.UTF8.GetBytes("Password");

            //Length of Salt
            int saltLengthLimit = 32;

            //Creation of Salt
            byte[] salt = new byte[saltLengthLimit];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }

            //Assigning Salt to patient
            patient.Salt = Convert.ToBase64String(salt);

            //Creation of Hash and generation of Hashed Password
            var passwordHash = SHA512.Create();
            var password = passwordHash.ComputeHash(Encoding.UTF8.GetBytes(patient.Password + patient.Salt + Environment.GetEnvironmentVariable("pepper")));

            //Assigning Hashed password to patient
            patient.Password = password;

            return patient;
        }
    }
}
