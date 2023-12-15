namespace TechBeyondThoughts.Web.Models
{
    public class EmailSettings
    {

        public string AdminEmail { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpHost { get; set; } // Updated property name
        public string SmtpUsername { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }
    }
}
