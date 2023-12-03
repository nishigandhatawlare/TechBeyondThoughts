namespace TechBeyondThoughts.Web.Utility
{
    public class SD
    {
        public static String TechAPIBase { get; set; }
		public static String AuthAPIBase { get; set; }


		public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }
    }
}
