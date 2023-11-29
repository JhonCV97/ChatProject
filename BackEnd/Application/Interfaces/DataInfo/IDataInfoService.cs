using Application.Common.Response;
using Application.Cqrs.DataInfo.Commands;
using Application.DTOs.DataInfo;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application.Interfaces.DataInfo
{
    public interface IDataInfoService
    {
        Task<ApiResponse<bool>> AddDataInfo(List<DataInfoPostDto> request);
        Task<ApiResponse<List<DataInfoDto>>> GetDataInfo();
        void DeleteDataInfo(int RoleId);
        Task<ApiResponse<bool>> AddDataInfoClassExcel(AddExcelClassCommand dataExcel);
        List<DataInfoPostDto> AddLocationData();
    }
}