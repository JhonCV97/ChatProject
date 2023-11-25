using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.DTOs.DataInfo;
using Application.DTOs.User;
using Application.Interfaces.DataInfo;
using AutoMapper;
using Azure.Core;
using Azure;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Application.Services.DataInfo
{
    public class DataInfoService : IDataInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly string _connectionString;
        public DataInfoService(IUnitOfWork unitOfWork, IMapper autoMapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _connectionString = configuration.GetConnectionString("ChatProjectConnection");
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


        public async Task<ApiResponse<List<DataInfoDto>>> GetDataInfo()
        {
            var response = new ApiResponse<List<DataInfoDto>>();

            try
            {
                response.Data = _autoMapper.Map<List<DataInfoDto>>(await _unitOfWork.DataInfoRepository.Get()
                                                                                                       .ToListAsync());
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al obtener los registros, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }

        public async void DeleteDataInfo(int RoleId)
        {
            string connectionString = _connectionString;

            string storedProcedureName = "DeleteDataInfo";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleId", RoleId);

                    try
                    {
                        await command.ExecuteReaderAsync();
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
        }
    }
}
