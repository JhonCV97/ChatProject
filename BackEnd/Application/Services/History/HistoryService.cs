using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.ConnectionChatGPT.Queries;
using Application.Cqrs.History.Commands;
using Application.Cqrs.History.Queries;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.History;
using Application.DTOs.User;
using Application.DTOs.UserHistory;
using Application.Interfaces.ConnectionChatGPT;
using Application.Interfaces.History;
using Application.Services.ConnectionChatGPT;
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
        private readonly IConnectionChatGPT _connectionChatGPT;
        public HistoryService(IUnitOfWork unitOfWork, IMapper autoMapper, IConnectionChatGPT connectionChatGPT)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _connectionChatGPT = connectionChatGPT;
        }

        public async Task<ApiResponse<List<UserHistoryDto>>> GetChatsBySession(GetHistoryQueryBySession request)
        {
            var response = new ApiResponse<List<UserHistoryDto>>();

            try
            {

                var HistoriesList = _autoMapper.Map<List<UserHistoryDto>>(await _unitOfWork.UserHistoryRepository.Get()
                                                                                                           .Where(x => x.History.ParentHistoryId == request.ChatId && x.UserId == request.UserId)
                                                                                                            .Include(u => u.User)
                                                                                                            .Include(h => h.History)
                                                                                                            .ToListAsync());
                if (request.ChatId == null)
                {
                    HistoriesList = HistoriesList.OrderByDescending(x => x.HistoryId).ToList();
                }

                response.Data = HistoriesList;
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
                var getMessageChatGPTQuery = new GetMessageChatGPTQuery
                {
                    Question = request.historyDtoPost.Question
                };

                var answer = await _connectionChatGPT.GetMessageChat(getMessageChatGPTQuery);

                request.historyDtoPost.Answer = answer.Data;

                response.Data = _autoMapper.Map<HistoryDto>(await _unitOfWork.HistoryRepository.Add(_autoMapper.Map<Domain.Models.History>(request.historyDtoPost)));
                
                var userHistoryPostDto = new UserHistoryPostDto
                {
                    HistoryId = response.Data.Id,
                    UserId = request.UserId,
                };

                await _unitOfWork.UserHistoryRepository.Add(_autoMapper.Map<Domain.Models.UserHistory>(userHistoryPostDto));

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

        public async Task<ApiResponse<bool>> DeleteHistory(DeleteHistoryCommand request)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var History = await _unitOfWork.HistoryRepository.GetById(request.Id);
                var UserHistory = await _unitOfWork.UserHistoryRepository.GetById(History.Id);

                var AllMessageHistory = await _unitOfWork.HistoryRepository.Get()
                                                                            .Where(p => p.ParentHistoryId == History.Id)
                                                                            .ToListAsync();

                if (AllMessageHistory.Count > 0)
                {
                    await DeleteAllMessage(AllMessageHistory);
                }

                await _unitOfWork.UserHistoryRepository.Delete(UserHistory);

                response.Data = await _unitOfWork.HistoryRepository.Delete(History);
                response.Result = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al eliminar el registro, consulte con el administrador. {ex.Message} ";
                throw;
            }
            return response;
        }

        public async Task<bool> DeleteAllMessage(List<Domain.Models.History> histories)
        {
            try
            {
                foreach (var history in histories)
                {
                    await _unitOfWork.HistoryRepository.Delete(history);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

    }
}
