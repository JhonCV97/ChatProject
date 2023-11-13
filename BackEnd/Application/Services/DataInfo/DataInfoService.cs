using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.DTOs.DataInfo;
using Application.DTOs.User;
using Application.Interfaces.DataInfo;
using AutoMapper;
using Domain.Interfaces;
using Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DataInfo
{
    public class DataInfoService : IDataInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        public DataInfoService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<ApiResponse<bool>> AddDataInfo(List<DataInfoPostDto> request)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var dataRequest = _autoMapper.Map<List<Domain.Models.DataInfo>>(request);
                await _unitOfWork.DataInfoRepository.AddRange(dataRequest);

                response.Data = true;
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear la Data. {ex.Message} ";
            }

            return response;
        }
    }
}
