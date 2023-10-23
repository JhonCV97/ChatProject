using Application.Common.Response;
using Application.Cqrs.History.Commands;
using Application.Cqrs.History.Queries;
using Application.DTOs.History;
using Application.DTOs.UserHistory;
using System.Threading.Tasks;

namespace Application.Interfaces.History
{
    public interface IHistoryService
    {
        Task<ApiResponse<HistoryDto>> AddHistory(PostHistoryCommand request);
        Task<ApiResponse<UserHistoryPostDto>> GetChatsBySession(GetHistoryQueryBySession request);
    }
}