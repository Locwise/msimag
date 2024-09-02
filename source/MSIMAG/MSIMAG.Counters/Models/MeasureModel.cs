using MSIMAG.Core.Entities;

namespace MSIMAG.Counters.Models
{
    public class MeasureModel
    {
        /// <summary>
        /// new pcflurentalunits
        public Guid Unit { get; set; }
        /// <summary>
        /// pcfsystemfield106
        /// new pcfplmainmeter
        /// </summary>
        public bool IsMainCounter { get; set; }
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

        /// <summary>
        /// pcfsystemfield104
        /// </summary>
        //public string Reading { get; set; }

        public IFormFile? FileUpload { get; set; }

        public string MeterReading { get; set; }
        public string MeterNumber { get; set; }

        public string RentalAgreement { get; set; }
        public DateTime Date { get; set; }


    }
    public static class MeasureModelExtensions
    {


        public static MeasureModel ToModel(this Measure measure)
        {

            return new MeasureModel
            {
                FileUpload = measure.FileUpload,
                Position = measure.Position,
                CounterType = measure.CounterType,
                Protocol = measure.Protocol,
                Unit = measure.Unit,
                Date = measure.Date,
                MeterReading = measure.MeterReading,
            };

        }
    }
}
