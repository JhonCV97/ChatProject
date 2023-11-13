using Application.Common.Response;
using Application.Cqrs.History.Commands;
using Application.DTOs.History;
using Application.Interfaces.History;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.DTOs.UserHistory;

namespace Application.Cqrs.History.Queries
{
    public class GetHistoryQueryBySession : IRequest<ApiResponse<List<UserHistoryDto>>>
    {
        public int? ChatId { get; set; }
        public int UserId { get; set; }
    }
    public class PostHistoryCommandHandler : IRequestHandler<GetHistoryQueryBySession, ApiResponse<List<UserHistoryDto>>>
    {
        private readonly IHistoryService _historyService;
        public PostHistoryCommandHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<ApiResponse<List<UserHistoryDto>>> Handle(GetHistoryQueryBySession request, CancellationToken cancellationToken)
        {
            return await _historyService.GetChatsBySession(request);
        }
    }
}
