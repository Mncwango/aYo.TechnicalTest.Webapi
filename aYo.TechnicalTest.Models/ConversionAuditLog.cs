using System;
using System.Collections.Generic;
using System.Text;

namespace aYo.TechnicalTest.Models
{
   public class ConversionAuditLog
    {
        public int Id { get; set; }
        public string Inputs { get; set; }
        public string Output { get; set; }
        public ConversionType ConversionType { get; set; }
        public string ConversionUnits { get; set; }
        public TimeSpan CreationDate { get; set; }

    }
}
