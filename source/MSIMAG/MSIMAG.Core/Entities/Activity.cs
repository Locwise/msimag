using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{

    public class ActivitiyCodes 
    {
        public const int NoSet = 0;
        public const int Rent = 1;
        public const int Deposit = 2;
    }

    /// <summary>
    /// Represnt Incoming Or
    /// </summary>
    public enum ActivityType 
    { 
        Debit=1,
        Credit=-1
    }

    public enum Currency { 
        Euro=1
    }

    public enum UpdateMethod 
    { 
        Auto=1,
        Manual=2
    }

    public enum ActivityStatus 
    { 
        Pending=1,
        OverDue=2,
        Completed=3
    }

    public class Activity
    {
        public Guid ID { get; set; }
        public Guid PayerOrPayee { get; set; }
        public Guid Rental { get; set; }
        public ActivityType ActivityType { get; set; }
        public decimal Amount { get; set; } = 0;        
        public Currency Currency { get; set; } = Currency.Euro;

        public decimal TotalAmount 
        { 
            get 
            { 
                if(ActivityType== ActivityType.Debit)
                    return Amount;
                return -1 * Amount;
            }
        }

        public UpdateMethod UpdateMethod { get; set; } = UpdateMethod.Auto;
        public int ActivityCode { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; } = String.Empty;

        public ActivityStatus Status { get; set; }

        public Guid Document { get; set; }

        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal[] Components { get; set; } = new decimal[5];
        public bool IsActive { get; set; } = true;
    }
}
