using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthernHealthAPI.Models2
{
    public class SearchPatient
    {
        public string Urnumber { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime Dob { get; set; }
    }
}
