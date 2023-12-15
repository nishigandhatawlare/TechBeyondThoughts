namespace TechBeyondThoughts.Web.Service
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string GetToken();
        void ClearToken();
                
    }
}
