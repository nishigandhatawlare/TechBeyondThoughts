using Newtonsoft.Json;
using System.Net;
using System.Text;
using TechBeyondThoughts.Web.Models;
using static TechBeyondThoughts.Web.Utility.SD;

namespace TechBeyondThoughts.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {

            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;


        }
        public async Task<ResponceDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("TechAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }


                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiResponce = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PATCH:
                        message.Method = HttpMethod.Patch;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponce = await client.SendAsync(message);

                switch (apiResponce.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponce.Content.ReadAsStringAsync();
                        var apiResponceDto = JsonConvert.DeserializeObject<ResponceDto>(apiContent);
                        return apiResponceDto;
                }

            }
            catch (Exception ex)
            {
                var dto = new ResponceDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }

        public async Task<ResponceDto?> SendPdfAsync(RequestDto requestDto, bool withBearer = true, FileType fileType = FileType.Pdf)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("TechAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                // Token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                // Request data
                if (requestDto.Data != null)
                {
                    switch (fileType)
                    {
                        case FileType.Json:
                            // For JSON requests
                            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                            break;
                            // Add additional cases for other file types if needed
                    }
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PATCH:
                        message.Method = HttpMethod.Patch;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponceDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponceDto { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponceDto { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponceDto { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.OK:
                        // Check the file type and handle accordingly
                        switch (fileType)
                        {
                            case FileType.Pdf:
                                var fileBytes = await apiResponse.Content.ReadAsByteArrayAsync();
                                return new ResponceDto { IsSuccess = true, Result = fileBytes };
                                // Add additional cases for other file types if needed
                        }
                        break;
                    default:
                        break;
                }

                return null;
            }
            catch (Exception ex)
            {
                var dto = new ResponceDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
        public async Task<PreviewResponceDto?> PreviewPdfAsync(RequestDto requestDto, bool withBearer = true, FileType fileType = FileType.Pdf)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("TechAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                // Token
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                // Request data
                if (requestDto.Data != null)
                {
                    switch (fileType)
                    {
                        case FileType.Json:
                            // For JSON requests
                            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                            break;
                            // Add additional cases for other file types if needed
                    }
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PATCH:
                        message.Method = HttpMethod.Patch;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new PreviewResponceDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new PreviewResponceDto { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new PreviewResponceDto { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new PreviewResponceDto { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.OK:
                        // Check the file type and handle accordingly
                        switch (fileType)
                        {
                            case FileType.Pdf:
                                var fileBytes = await apiResponse.Content.ReadAsByteArrayAsync();
                                var jsonString = await apiResponse.Content.ReadAsStringAsync();
                                var apiResult = JsonConvert.DeserializeObject<PreviewResponceDto>(jsonString);

                                return new PreviewResponceDto
                                {
                                    IsSuccess = apiResult.IsSuccess,
                                    FileContents = fileBytes,
                                    FileName = apiResult.FileName,
                                    PagesToPreview = apiResult.PagesToPreview,
                                    Result = apiResult.Result,
                                    Message = apiResult.Message
                                    // Add any other properties you need from the API response
                                };
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

                return null;
            }
            catch (Exception ex)
            {
                var dto = new PreviewResponceDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }

        public enum FileType
        {
            Json,
            Pdf,
            // Add additional file types if needed
        }

    }
}
