
using Locwise.Fireberry.Client;
using MSIMAG.ChargingEngine;
using MSIMAG.Core.Data;
using MSIMAG.Core.Entities;



namespace MSIMAG.ChargingEnigne
{
    public class Program 
    {

        /// <summary>
        /// TODO: Move value to configuration
        /// </summary>
        private const string API_KEY = @"e460a841-58ce-43cf-9cfc-6b8d5c43c2e3";

        public static int Main(string[] args)
        {
            Console.WriteLine("MSIMAG Charging Engine Started");

            DateTime stopDate= DateTime.Now;

            try 
            { 
                CreateCharges(stopDate); 
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());   
                return -1;
            }
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        public static int CreateCharges(DateTime stopDate) 
        { 
            IFireberryClient client = null;
            try
            {
                IFireberryClientFactory factory = new FireberryClientFactory();
                client = factory.Create(API_KEY,false);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed To Create Client",innerException:ex);
            }
            
            IDataManager dataManager=new DataManager(client);
            var rentals=dataManager.GetAllRentals().ToList();
            Console.WriteLine($"#Rental Found:{rentals.Count}");
            var result=new List<Activity>();
            rentals.ForEach(r =>
            {
                CalculationContext ctx = new CalculationContext(r)
                {
                    EndDate= stopDate,
                    Activities=dataManager.GetAllActivitiesByRental(r.ID).Where(a=>a.IsActive).ToList(),
                    Taarifs = dataManager.GetAllTaarifByRental(r.ID).ToList()
                };
                         
                var res=Process(ctx);
                if(res != null && res.Count()>0) 
                {
                    result.AddRange(res);
                }
            });

            Console.WriteLine($"Total No Of Activities Created:{result.Count}");

            return 0;
        }

        public static IEnumerable<Activity> Process(CalculationContext ctx) 
        {
            #region Handle Rent Charges
            
            var last = ctx.Activities.Where(a => a.ActivityCode == ActivitiyCodes.Rent)
                .MaxBy(x => x.PeriodEnd);


            IActiveDaysCalculator activeDaysCalculator=new ActiveDaysCalculator();
            var daysflags = activeDaysCalculator.Calculate(ctx.Rental, DateTime.Now, ctx.EndDate);
            return new Activity[] { };

            #endregion
        }

    }
}
