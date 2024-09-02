using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class Rental
    {
        public Guid Payer { get; set; }
        public Guid ID { get; set; }

        #region Dates
        public DateTime?  ListingDate { get;}
        public DateTime? VisitDate { get; set; }
        public DateTime? ApplicationDeadline { get; set; }
        public DateTime? LeaseSigningDate { get; set; }
        public DateTime? SecurityDepositAndFirstMonthRentPaymentDate { get; set; }
        public DateTime? HandoverKeyDate { get; set; }
        public DateTime? RentDueDate { get; set; }
        public DateTime? RenewalNoticeDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        public DateTime? MoveOutDate { get;set; }
        public DateTime? SecurityDepositReturnDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? CancelationDate { get;}
        public DateTime? InspectionDate { get; set; }
        #endregion
    }
}
