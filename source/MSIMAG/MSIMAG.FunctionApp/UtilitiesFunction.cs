using Locwise.Fireberry.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MSIMAG.Core.Data;
using MSIMAG.FunctionApp.AirCall;
using System.Net;

namespace MSIMAG.FunctionApp
{
    public class UtilitiesFunction
    {
        private readonly ILogger<UtilitiesFunction> _logger;
        private readonly IDataManager _dataManager;
        public UtilitiesFunction(IDataManager dataManager, ILogger<UtilitiesFunction> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        /// <summary>
        /// Syn Contact With AirCall
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Function("SyncContact")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post",Route ="accounts/{id}/sync")] HttpRequest req, string id)
        {
            _logger.LogInformation("C# HTTP trigger Sync Contact processed a request.");

            Guid accountid;
            if (!Guid.TryParse(id, out accountid))
            {
                return new BadRequestResult();
            }

            var account=_dataManager.GetAccountByID(accountid);
            if(account==null)
            {
                return new NotFoundResult();
            }

           


            var contactmodel = account.ToContactModel();
            if (contactmodel == null)
            {
                return new NoContentResult();
            }

            var aircall_client = new AirCallClient("ZjM2ZjcxZjM2NzIxZGZjMjM1NDliMDc4MDZkMTkxMjY6YWQ3YmQwNDRiNTgzYmMzOWMxOWViYmI4MGRjYzFhMTM=");

            if (contactmodel.id !=null && contactmodel.id != 0)
            {
                var res=aircall_client.UpdateSharedContact(contactmodel);
                if (res)
                {
                    return new OkResult();
                }
                else 
                {
                    throw new Exception("Error Calling AirCall");
                }
            }
            else 
            {
                var res = aircall_client.CreateSharedContact(contactmodel);
                if (res.Item1 == true)
                {

                }
                else 
                { 
                
                
                }
            }



            return new OkResult();            
        }
    }
}
