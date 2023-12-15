namespace TechBeyondThoughts.Web.Utility
{
    public class SD
    {
        public static String TechAPIBase { get; set; }
		public static String AuthAPIBase { get; set; }
        public static String ContactAPIBase { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "USER";
        public const string TokenCookie = "JWTToken";


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
