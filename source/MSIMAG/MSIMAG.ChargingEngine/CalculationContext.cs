using MSIMAG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.ChargingEngine
{
    public class CalculationContext
    {
        public CalculationContext(Rental rental)
        {
                Rental = rental;
        }
        public Rental Rental { get; set; }
        public List<Taarif> Taarifs { get; set; } = new List<Taarif>() { };
        public List<Activity> Activities { get; set; } = new List<Activity>() { };

        public DateTime StartDate { get; set; }
           public DateTime EndDate { get; set; }
    }
}
