using Application.DTOs.Role;
using Application.DTOs.User;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserPostDto>();
            CreateMap<Role, RoleDto>();
            
        }
    }
}
