using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.History.Commands;
using Application.Cqrs.History.Queries;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.History;
using Application.DTOs.User;
using Application.DTOs.UserHistory;
using Application.Interfaces.History;
using AutoMapper;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.History
{
    public class HistoryService : IHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        public HistoryService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<ApiResponse<UserHistoryPostDto>> GetChatsBySession(GetHistoryQueryBySession request)
        {
            var response = new ApiResponse<UserHistoryPostDto>();

            try
            {
                response.Data = _autoMapper.Map<UserHistoryPostDto>(await _unitOfWork.UserHistoryRepository.Get()
                                                                                                           .Where(x => x.History.ParentHistoryId == request.ChatId)
                                                                                                            .Include(u => u.User)
                                                                                                            .Include(h => h.History)
                                                                                                            .ToListAsync());
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }


        public async Task<ApiResponse<HistoryDto>> AddHistory(PostHistoryCommand request)
        {
            var response = new ApiResponse<HistoryDto>();

            try
            {
                response.Data = _autoMapper.Map<HistoryDto>(await _unitOfWork.HistoryRepository.Add(_autoMapper.Map<Domain.Models.History>(request.historyDtoPost)));
                await _unitOfWork.UserHistoryRepository.Add(_autoMapper.Map<Domain.Models.UserHistory>(request.userHistoryPostDto));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear Usuario. {ex.Message} ";
            }

            return response;
        }

    }
}
