namespace TechBeyondThoughts.Web.Models
{
    public class DownloadHistoryDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime DownloadDateTime { get; set; }

    }
}
