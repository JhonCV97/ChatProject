using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.User
{
    public interface IUserService
    {
        Task<ApiResponse<UserDto>> AddUser(PostUserCommand request);
        Task<ApiResponse<List<UserDto>>> GetUsers();
        Task<ApiResponse<UserDto>> UpdateUser(PutUserCommand request);
        Task<ApiResponse<bool>> DeleteUser(DeleteUserCommand request);
        Task<ApiResponse<UserDto>> GetUsersById(GetUsersQueryById request);
    }
}