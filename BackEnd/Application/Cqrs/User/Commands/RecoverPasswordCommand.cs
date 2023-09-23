using Application.Common.Response;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.SendEmail;

namespace Application.Cqrs.User.Commands
{
    public class RecoverPasswordCommand : IRequest<ApiResponse<bool>>
    {
        public string Email { get; set; }
    }

    public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, ApiResponse<bool>>
    {
        private readonly ISendEmailService _sendEmailService;
        public RecoverPasswordCommandHandler(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        public async Task<ApiResponse<bool>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _sendEmailService.RecoverPassword(request);
        }
    }
}
