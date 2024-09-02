using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIMAG.Counters.Models;

namespace MSIMAG.Counters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCallerInfo() { 
            return Ok(new UserInfoModel() { 
                FullName="Sion Cohen"
            });
        }
    }
}
