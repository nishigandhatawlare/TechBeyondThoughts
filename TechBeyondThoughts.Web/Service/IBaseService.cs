using Microsoft.VisualBasic.FileIO;
using TechBeyondThoughts.Web.Models;
using static TechBeyondThoughts.Web.Service.BaseService;

namespace TechBeyondThoughts.Web.Service
{
    public interface IBaseService
    {
        Task<ResponceDto?> SendAsync(RequestDto requestDto,bool withBearer = true);
        Task<ResponceDto?> SendPdfAsync(RequestDto requestDto, bool withBearer = true, FileType fileType = FileType.Pdf);
        Task<PreviewResponceDto?> PreviewPdfAsync(RequestDto requestDto, bool withBearer = true, FileType fileType = FileType.Pdf);

    }
}
