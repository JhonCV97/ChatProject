using Application.Common.Response;
using Application.DTOs.DataInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.DataInfo
{
    public interface IDataInfoService
    {
        Task<ApiResponse<bool>> AddDataInfo(List<DataInfoPostDto> request);
    }
}