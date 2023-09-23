using Application.Common.Response;
using Application.DTOs.User;
using Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Cqrs.User.Queries
{
    public class GetUsersQueryById : IRequest<ApiResponse<UserDto>>
    {
        public int Id { get; set; }
    }
    public class GetUsersQueryByIdHandler : IRequestHandler<GetUsersQueryById, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;
        public GetUsersQueryByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(GetUsersQueryById request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersById(request);
        }
    }
}
