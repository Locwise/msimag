using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIMAG.Counters.Models;

using Google.Cloud.Vision.V1;


namespace MSIMAG.Counters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadImage([FromForm]FileModel fileModel) 
        {
            ImageUploadResult result=new ImageUploadResult();
            try
            {
                Image img = Image.FromStream(fileModel.File.OpenReadStream());
                ImageAnnotatorClient client = ImageAnnotatorClient.Create();
               
                var res=client.DetectText(img);
                res.ToList().ForEach(e => {
                    



                });





            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage=ex.Message;
            }


            return Ok(result);
        }


    }

    public class ImageUploadResult 
    {
        public bool Success { get; set; }
        public string CounterID { get; set; }
        public string Measure { get; set; }
        public string ErrorMessage { get; set; }=String.Empty;
        public string Url { get; set; }

    }
}
