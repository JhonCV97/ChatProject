using Application.Common.Response;
using Application.Cqrs.DataInfo.Commands;
using Application.DTOs.DataInfo;
using Application.Interfaces.DataInfo;
using AutoMapper;
using ClosedXML.Excel;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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


        public List<List<string>> UploadExcel(AddExcelClassCommand request)
        {
            try
            {
                var PathExcel = SaveExcel(request);

                using (var workbook = new XLWorkbook(PathExcel))
                {
                    var worksheet = workbook.Worksheet(1);

                    var worksheetList = worksheet.RowsUsed()
                                                        .Select(row => row.Cells()
                                                            .Select(cell => cell.Value.ToString())
                                                            .ToList())
                                                        .ToList();

                    return worksheetList;
                }

            }
            catch (Exception ex)
            {

            }

            return null;

        }

        public async Task<ApiResponse<bool>> AddDataInfoClassExcel(AddExcelClassCommand request)
        {
            var response = new ApiResponse<bool>();
            List<DataInfoPostDto> DataInfoList = new List<DataInfoPostDto>();
            var correctData = false;
            var headers = new[] { "asignatura", "codigo", "no clase", "salo", "modalida", "departament", "no_hora",
                                    "horari", "fecha_inici", "fecha_f", "horas de cl", "sesion", "docente sugerid" };

            var dataExcel = UploadExcel(request);

            for (int i = 0; i < dataExcel.Count(); i++)
            {
                var data = dataExcel.ElementAt(i);

                if (correctData)
                {
                    var DataInfo = new DataInfoPostDto
                    {
                        QueryType = "Clases",
                        Response = "Asiganatura: "+ data[0] + " Salon: "+ data[3]+ " Fecha Inicio: "+data[8]+ " Docente: "+ data[12],
                        RoleId = 3
                    };

                    DataInfoList.Add(DataInfo);
                }



                if (i == 0)
                {
                    var Validateheaders = headers.Select(h => h.Trim().ToLower())
                                .All(h => data.Any(d => d.ToString().ToLower().Contains(h)));

                    if (Validateheaders)
                    {
                        correctData = true;
                    }
                    else
                    {
                        response.Data = false;
                        response.Result = false;
                        response.Message = "Por favor ingrese un Excel Correcto";

                        return response;
                    }
                }


            }

            DeleteDataInfo(3);
            await AddDataInfo(DataInfoList);

            response.Data = true;
            response.Result = true;
            response.Message = "OK";

            return response;
        }


        public string SaveExcel(AddExcelClassCommand request)
        {
            string UrlPath = @"Resources\\PublicFiles\\Excel";
            string Pathfinal = Path.Combine(Directory.GetCurrentDirectory(), UrlPath);

            var fullPath = "";

            if (request.File.Length > 0)
            {
                if (!Directory.Exists(Pathfinal))
                    Directory.CreateDirectory(Pathfinal);

                fullPath = Path.Combine(Pathfinal, request.File.FileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                request.File.CopyTo(stream);
            }

            return fullPath;
        }

        public List<DataInfoPostDto> AddLocationData()
        {
            List<DataInfoPostDto> DataInfoList = new List<DataInfoPostDto>()
            {
                new DataInfoPostDto
                {
                    QueryType = "Biblioteca Principal",
                    Response = "La Biblioteca Principal se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Punto de Información",
                    Response = "Punto de Información se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Rectoría",
                    Response = "La rectoría se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Teatro 8 de junio",
                    Response = "Teatro 8 de junio se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Sala Carlos Nader",
                    Response = "La Sala Carlos Nader se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Registro académico",
                    Response = "El Registro académico se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "División financiera",
                    Response = "La División financiera se encuentra en Edifico Administrativo también conocido como bloque A",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Vicerrectoría",
                    Response = "La Vicerrectoría se encuentra en edificio central bloque B",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Facultad de artes y humanidades",
                    Response = "La Facultad de artes y humanidades se encuentra en edificio central bloque C",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Facultad de ingeniería",
                    Response = "La Facultad de ingeniería se encuentra en el edificio del parque o también conocido como bloque D",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Laboratorios",
                    Response = "Los laboratorios se encuentran en el edificio de laboratorio o también conocido como bloque E",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Sede sancacio",
                    Response = "La sede sancacios e encuentra en el bloque F",
                    RoleId = 2
                },
                new DataInfoPostDto
                {
                    QueryType = "Facultad de ciencias agropecuarias",
                    Response = "La facultad de ciencias agropecuarias se encuentra en la sede sancacio",
                    RoleId = 2
                }

            };

            return DataInfoList;

        }

    }
}
