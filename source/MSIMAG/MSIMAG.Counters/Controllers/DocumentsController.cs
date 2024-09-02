using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Services;
using Google.Apis.Document.v1;
using Google.Cloud.DocumentAI.V1;
using Google.Protobuf;


namespace MSIMAG.Counters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {

        private string projectId = "your-project-id";
        private string locationId = "your-processor-location";
        private string processorId = "your-processor-id";
        private string localPath = "my-local-path/my-file-name";
        private string mimeType = "application/pdf";





        public IActionResult ParseDocument([FromForm]IFormFile file) 
        {
           var credentials= new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "[YOUR_API_KEY_HERE]",
            };



            // Read in local file
            using var fileStream = System.IO.File.OpenRead(localPath);
            var rawDocument = new RawDocument
            {
                Content = ByteString.FromStream(fileStream),
                MimeType = mimeType
            };

            // Create client
            var client = new DocumentProcessorServiceClientBuilder
            {
                Endpoint = $"{locationId}-documentai.googleapis.com"
            }.Build();

            var request = new ProcessRequest
            {
                Name = ProcessorName.FromProjectLocationProcessor(projectId, locationId, processorId).ToString(),
                RawDocument = rawDocument
            };


            // Make the request
            var response = client.ProcessDocument(request);

            var document = response.Document;
            Console.WriteLine(document.Text);
           
            return new OkObjectResult(document);
        
        }
    }
}
