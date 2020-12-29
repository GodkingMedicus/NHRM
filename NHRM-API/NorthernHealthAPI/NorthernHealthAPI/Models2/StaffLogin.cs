using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthernHealthAPI.Models2
{
    public class StaffLogin
    {
        public string userID { get; set; }
        public string password { get; set; }
        public string? Role { get; set; }
    }
}
