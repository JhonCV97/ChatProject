using Application.Common.Response;
using Application.DTOs.User;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Cqrs.User.Commands
{
    public class PutUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public UserDto UserDto { get; set; }
    }
    public class PutUserCommandHandler : IRequestHandler<PutUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;
        public PutUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(PutUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request);
        }
    }
}
