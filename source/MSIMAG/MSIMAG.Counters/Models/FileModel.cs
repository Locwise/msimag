namespace MSIMAG.Counters.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public int FileType { get; set; } = 0;
    }
}
