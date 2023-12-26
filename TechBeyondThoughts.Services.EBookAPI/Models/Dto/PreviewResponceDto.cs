namespace TechBeyondThoughts.Services.EBookAPI.Models.Dto
{
    public class PreviewResponceDto
    {
        public string FileName { get; set; }
        public byte[] FileContents { get; set; }
        public int PagesToPreview { get; set; }
        // Add any other properties you need for the preview response
        public object? Result { get; set; }

        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";

    }
}
