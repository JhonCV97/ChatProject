using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.History;

namespace Application.Cqrs.History.Commands
{
    public class DeleteHistoryCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }

    public class DeleteHistoryCommandHandler : IRequestHandler<DeleteHistoryCommand, ApiResponse<bool>>
    {
        private readonly IHistoryService _historyService;
        public DeleteHistoryCommandHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteHistoryCommand request, CancellationToken cancellationToken)
        {
            return await _historyService.DeleteHistory(request);
        }
    }
}
