using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
    public class  ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserPostDto, User>();
            CreateMap<RoleDto, Role>();

        }
    }
}
