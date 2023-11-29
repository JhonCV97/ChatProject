using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.DTOs.User;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.DTOs.History;
using Application.Interfaces.History;
using Application.DTOs.UserHistory;

namespace Application.Cqrs.History.Commands
{
    public class PostHistoryCommand : IRequest<ApiResponse<HistoryDto>>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public HistoryDtoPost historyDtoPost { get; set; }
    }
    public class PostHistoryCommandHandler : IRequestHandler<PostHistoryCommand, ApiResponse<HistoryDto>>
    {
        private readonly IHistoryService _historyService;
        public PostHistoryCommandHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<ApiResponse<HistoryDto>> Handle(PostHistoryCommand request, CancellationToken cancellationToken)
        {
            return await _historyService.AddHistory(request);
        }
    }
}
