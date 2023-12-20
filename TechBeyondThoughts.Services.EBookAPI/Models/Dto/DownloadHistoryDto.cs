namespace TechBeyondThoughts.Services.EBookAPI.Models.Dto
{
    public class DownloadHistoryDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DownloadDateTime { get; set; }

    }
}
