using System;
using System.Collections.Generic;

namespace NorthernHealthAPI.Models2
{
    public class PatientMeasurementRecord
    {
        public double Value { get; set; }
        public DateTime DateTimeRecorded { get; set; }

        public int MeasurementId { get; set; }
    }
}
