using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public class NewsService : INewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiKey = "69c63d53452d49b5b777e1b0bad4b1d6"; // Replace with your actual API key

        public NewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<NewsApiResponse> GetTechnologyNewsAsync()
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://newsapi.org/v2/");

                    // Set User-Agent header
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "TechBeyondThoughts.Web");

                    var apiUrl = $"top-headlines?country=in&category=technology&apiKey={apiKey}";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<NewsApiResponse>(jsonResponse);
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error Response: {errorResponse}");
                        return null;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HttpRequestException: {ex.Message}");
                return null;
            }
        }
    }
}
