using Locwise.Fireberry.Client;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MSIMAG.Core.Data;
using MSIMAG.Core.Entities;
using MSIMAG.Counters.Models;
using System.Diagnostics;

namespace MSIMAG.Counters.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     

        public HomeController(ILogger<HomeController> logger 
            )
        {
            _logger = logger;
         
        }

        public IActionResult Index()
        {
            return View();
        }

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


     
    }
}