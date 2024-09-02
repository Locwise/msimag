using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class RentalAgreement
    {
        //"pcfsystemfield100", "name", "pcfstatusoffera", "pcfstatusreason"
        public Guid ID { get; set; }
        public string StatuReason { get; set; }//pcfstatusreason
        public string RentalOffersName { get; set; }//name
        public string AgreementStatus { get; set; }//   
        public string StatusOfferA { get; set;} //pcfstatusoffera
        //public Guid UnitId { get; set; }

    }
}
