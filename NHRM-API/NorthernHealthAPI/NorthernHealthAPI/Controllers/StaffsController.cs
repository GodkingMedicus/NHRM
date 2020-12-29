using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NorthernHealthAPI.Models2;

namespace NorthernHealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public StaffsController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        //Returns a SearchStaff object (Staff object without Salt & Password)
        [HttpPost, Route("search")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff(Staff pt)
        {
            List<Staff> staff = new List<Staff>();

            if (pt.StaffId.ToString() != "")
            {
                Console.WriteLine("HospitalSearch");
                //staff = await _context.Staff.Where(x => x.Urnumber == pt.Urnumber).ToListAsync();
                staff = await (from s in _context.Staff
                               where s.Email == pt.Email
                               select new Staff
                               {
                                   Email = s.Email,
                                   FirstName = s.FirstName,
                                   Surname = s.Surname,
                                   Password = null,
                                   Salt = null,
                                   RoleId = s.RoleId
                               }).ToListAsync();
            }

            else if (pt.FirstName != "" && pt.Surname != "")
            {
                Console.WriteLine("F+S Search");
                //staff = await _context.Staff.Where(x => x.FirstName == pt.FirstName && x.SurName == pt.SurName).ToListAsync();
                staff = await (from s in _context.Staff
                               where s.Email == pt.Email
                               select new Staff
                               {
                                   Email = s.Email,
                                   FirstName = s.FirstName,
                                   Surname = s.Surname,
                                   Password = null,
                                   Salt = null,
                                   RoleId = s.RoleId
                               }).ToListAsync();
            }
            else if (pt.Surname != "")
            {
                Console.WriteLine("S Search");
                //staff = await _context.Staff.Where(x => x.SurName == pt.SurName).ToListAsync();
                staff = await (from s in _context.Staff
                               where s.Email == pt.Email
                               select new Staff
                               {
                                   Email = s.Email,
                                   FirstName = s.FirstName,
                                   Surname = s.Surname,
                                   Password = null,
                                   Salt = null,
                                   RoleId = s.RoleId
                               }).ToListAsync();
            }
            else if (pt.FirstName != "")
            {
                Console.WriteLine("F Search");
                //staff = await _context.Staff.Where(x => x.FirstName == pt.FirstName).ToListAsync();
                staff = await (from s in _context.Staff
                               where s.Email == pt.Email
                               select new Staff
                               {
                                   FirstName = s.FirstName,
                                   Surname = s.Surname,
                                   Password = null,
                                   Salt = null,
                                   RoleId = s.RoleId
                               }).ToListAsync();
            }

            if (staff == null || staff.Count < 1)
            {
                Console.WriteLine("NotFound");
                return NotFound();
            }

            return staff;
        }

        // GET: api/Staffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(string id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstaff(string id, Staff staff)
        {
            /*if (id != staff.Urnumber)
            {
                return BadRequest();
            }*/

            //_context.Entry(staff).State = EntityState.Modified;

            string query = "UPDATE staff SET " +
                "FirstName=@FirstName, " +
                "Surname=@Surname, " +
                "RoleID=@RoleId WHERE Email=@Email";

            _context.Database.ExecuteSqlRaw(query,
                new SqlParameter("@FirstName", staff.FirstName),
                new SqlParameter("@Surname", staff.Surname),
                new SqlParameter("@RoleId", staff.RoleId),
                new SqlParameter("@Email", id)

                );



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // POST: api/Staffs/Password
        [HttpPut, Route("Password")]
        public async Task<ActionResult<Staff>> PutStaffPassword( Staff staff)
        {
            GenerateSaltAndPassword(staff);

            string query = "UPDATE staff SET " +
                "Password=@Password, " +
                "Salt=@Salt WHERE Email=@Email";

            _context.Database.ExecuteSqlRaw(query,
                new SqlParameter("@Password", staff.Password),
                new SqlParameter("@Salt", staff.Salt),
                new SqlParameter("@Email", staff.Email)

                );



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(staff.Email))
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

        // POST: api/Staffs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            Console.WriteLine("test");
            staff = GenerateSaltAndPassword(staff);

            _context.Staff.Add(staff);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StaffExists(staff.Email.ToString()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStaff", new { id = staff.StaffId }, staff);
        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Staff>> DeleteStaff(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return staff;
        }

        private bool StaffExists(string id)
        {
            return _context.Staff.Any(e => e.StaffId.ToString() == id);
        }

        private Staff GenerateSaltAndPassword(Staff staff)
        {
            //Length of Salt
            int saltLengthLimit = 32;

            //Creation of Salt
            byte[] salt = new byte[saltLengthLimit];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetBytes(salt);
            }

            //Assigning Salt to staff
            staff.Salt = Convert.ToBase64String(salt);

            //Creation of Hash and generation of Hashed Password
            var passwordHash = SHA512.Create();
            var password = passwordHash.ComputeHash(Encoding.UTF8.GetBytes(staff.Password + staff.Salt + Environment.GetEnvironmentVariable("pepper")));

            //Assigning Hashed password to staff
            staff.Password = password;

            return staff;
        }
    }
}
