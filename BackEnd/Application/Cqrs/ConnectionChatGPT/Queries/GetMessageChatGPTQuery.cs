using Application.Common.Response;
using Application.Cqrs.History.Queries;
using Application.DTOs.UserHistory;
using Application.Interfaces.History;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.ConnectionChatGPT;

namespace Application.Cqrs.ConnectionChatGPT.Queries
{
    public class GetMessageChatGPTQuery : IRequest<ApiResponse<string>>
    {
        public string Question { get; set; }
        public int RoleId { get; set; }
    }
    public class GetMessageChatGPTQueryHandler : IRequestHandler<GetMessageChatGPTQuery, ApiResponse<string>>
    {
        private readonly IConnectionChatGPT _connectionChatGPT;
        public GetMessageChatGPTQueryHandler(IConnectionChatGPT connectionChatGPT)
        {
            _connectionChatGPT = connectionChatGPT;
        }

        public async Task<ApiResponse<string>> Handle(GetMessageChatGPTQuery request, CancellationToken cancellationToken)
        {
            return await _connectionChatGPT.GetMessageChat(request);
        }
    }
}
