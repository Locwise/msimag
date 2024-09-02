using MSIMAG.Core.Entities;

namespace MSIMAG.Counters.Models
{
    public class RentalAgreementModel
    {
        public Guid ID { get; set; }
        public string StatuReason { get; set; }//pcfstatusreason
        public string RentalOffersName { get; set; }//name
        public string AgreementStatus { get; set; }//   
        public string StatusOfferA { get; set; } //pcfstatusoffera
        //public Guid UnitId { get; set; }
    }

    public static class RentalAgreementModelExtensions
    {


        public static RentalAgreementModel ToModel(this RentalAgreement ra)
        {

            return new RentalAgreementModel
            {
                ID = ra.ID,
                StatuReason = ra.StatuReason,
                RentalOffersName = ra.RentalOffersName,
                AgreementStatus = ra.AgreementStatus,
                StatusOfferA = ra.StatusOfferA,


            };

        }
    }
}
