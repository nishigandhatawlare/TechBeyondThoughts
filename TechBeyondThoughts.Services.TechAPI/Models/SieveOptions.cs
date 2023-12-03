namespace TechBeyondThoughts.Services.TechAPI.Models
{
	public class SieveOptions
	{
		public int PageSize { get; set; }
		public int MaxPageSize { get; set; }
		public bool ThrowExceptions { get; set; }
		public int CacheDuration { get; set; }
	}
}
