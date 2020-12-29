using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using NorthernHealthAPI.Models2;

namespace NorthernHealthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly nhrmappdbContext _context;

        public AuthController(nhrmappdbContext context)
        {
            _context = context;
        }

        // GET api/auth/login
        // Accepts a Login object - parameters userId (string) and password (string). If the model values match a user login in the database it
        // returns a JWT, otherwise Unauthorized result if details invalid, BadRequest if login details improper
        //[HttpPost, Route("patient")]
        //public IActionResult Login(Login login)
        //{
        //    if (login == null)
        //    {
        //        return BadRequest(new { message = "Invalid client request" });
        //    }

        //    var passwordHash = SHA512.Create();
        //    passwordHash.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

        //    var patient = (from p in _context.Patient
        //                   where p.Urnumber == login.UserId && p.Password == passwordHash.Hash
        //                   select new Patient { Urnumber = p.Urnumber });

        //    if (patient.Count() != 0)
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret")));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        var claims = new[] {
        //            new Claim(ClaimTypes.Role, "Patient")
        //        };

        //        var tokenOptions = new JwtSecurityToken(
        //            issuer: Environment.GetEnvironmentVariable("applicationUrl"),
        //            audience: Environment.GetEnvironmentVariable("applicationUrl"),
        //            claims: claims,
        //            expires: DateTime.Now.AddDays(5),
        //            signingCredentials: signinCredentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //        return Ok(new { Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        [HttpPost, Route("admin")]
        public IActionResult Login(StaffLogin login)
        {
            Console.WriteLine("test2");

            if (login == null)
            {
                Console.WriteLine("test3");
                return BadRequest(new { message = "Invalid client request" });
            }

            Console.WriteLine("test4");
            var staff = (from s in _context.Staff
                             //where s.Email == login.Email
                         where s.Email == login.userID
                           select new Staff
                           {
                               StaffId = s.StaffId,
                               Email = s.Email,
                               Salt = s.Salt,
                               Password = s.Password,
                               RoleId = s.RoleId
                           }).ToList();

            Console.WriteLine("test5");
            //Handle invalid logins
            if (staff.Count == 0)
            {
                Console.WriteLine("test6");
                return BadRequest(new { message = "Username or Password is incorrect" });
            }

            Console.WriteLine("test7");
            var passwordHash = SHA512.Create();

            passwordHash.ComputeHash(Encoding.UTF8.GetBytes(Encoding.UTF8.GetBytes(login.password) + staff.SingleOrDefault().Salt + Environment.GetEnvironmentVariable("pepper")));

            Console.WriteLine("test1");
            if (passwordHash.Hash.SequenceEqual(staff.SingleOrDefault().Password))
            {
                Console.WriteLine("test");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[] {
                    //new Claim("Role", staff.FirstOrDefault().Role.ToString()),
                    new Claim("StaffID", staff.FirstOrDefault().StaffId.ToString()),
                    new Claim("Roleid", staff.FirstOrDefault().RoleId.ToString())
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: Environment.GetEnvironmentVariable("applicationUrl"),
                    audience: Environment.GetEnvironmentVariable("applicationUrl"),
                    claims: claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });
            }
            else
            {
                Console.WriteLine("Failed Login");
                return Unauthorized();
            }
        }
    }
}
