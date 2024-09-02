using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class Measure
    {
        /// <summary>
        /// new pcflurentalunits
        public Guid Unit { get; set; }

        /// <summary>
        /// pcfsystemfield102
        /// new pcfplposition
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// customobject1021
        /// new pcfplmetertype
        /// </summary>
        public string CounterType { get; set; }

        /// <summary>
        /// pcfsystemfield107
        /// new pcfplprotokol
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// pcfsystemfield103
        /// </summary>
        //public string CounterNumber { get; set; }

        public IFormFile? FileUpload { get; set; }
        public string MeterReading { get; set; }
        public string MeterNumber { get; set; }
        public string IsMainCounter { get; set; }

        public string RentalAgreement { get; set; }
        public DateTime Date { get;set; }

    }
}
