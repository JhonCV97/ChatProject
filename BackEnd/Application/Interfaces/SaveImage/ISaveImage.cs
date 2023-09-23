using Application.Common.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Interfaces.SaveImage
{
    public interface ISaveImage
    {
        Task<ApiResponse<string>> SaveImg(IFormFile file);
    }
}