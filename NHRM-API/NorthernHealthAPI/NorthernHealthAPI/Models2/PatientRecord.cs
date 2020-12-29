using System;
using System.Collections.Generic;

namespace NorthernHealthAPI.Models2
{
    public partial class PatientRecord
    {
        public DateTime DateTimeRecorded { get; set; }
        public string Notes { get; set; }
        public string Urnumber { get; set; }
        public int RecordTypeId { get; set; }

        public virtual RecordType RecordType { get; set; }
        public virtual Patient UrnumberNavigation { get; set; }
    }
}
