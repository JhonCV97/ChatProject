using Application.Common.Response;
using Application.DTOs.UserHistory;
using Application.Interfaces.History;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.DTOs.Report;

namespace Application.Cqrs.History.Queries
{
    public class ReportHistoryQuery : IRequest<ApiResponse<Report>>
    {

    }
    public class ReportHistoryQueryHandler : IRequestHandler<ReportHistoryQuery, ApiResponse<Report>>
    {
        private readonly IHistoryService _historyService;
        public ReportHistoryQueryHandler(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<ApiResponse<Report>> Handle(ReportHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _historyService.ReportHistory();
        }
    }
}
