namespace TechBeyondThoughts.Web.Models
{
    public class PreviewResponceDto
    {
         public string FileName { get; set; }
        public byte[] FileContents { get; set; }
        public int PagesToPreview { get; set; }
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";

    }
}
