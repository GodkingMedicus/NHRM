using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthernHealthAPI.Models
{
    public class UpdatePatient
    {

        public string Urnumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string CountryOfBirth { get; set; }
        public string PreferredLanguage { get; set; }
        public bool LivesAlone { get; set; }
        public string RegisteredBy { get; set; }
        public bool Active { get; set; }

    }
}
