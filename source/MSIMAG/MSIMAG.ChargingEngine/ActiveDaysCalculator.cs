using Microsoft.Extensions.Primitives;
using MSIMAG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.ChargingEngine
{ 
    public class ActiveDaysCalculator : IActiveDaysCalculator
    {
        public int[] Calculate(Rental rental , DateTime start, DateTime end)
        {
            return new int[] { };
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Returns 
    /// </summary>
    internal interface IActiveDaysCalculator 
    {
        //DateTimeOffset 
        int[] Calculate(Rental rental, DateTime start, DateTime end);        
    }
}
