using Application.DTOs.DataInfo;
using Application.DTOs.History;
using Application.DTOs.Role;
using Application.DTOs.User;
using Application.DTOs.UserHistory;
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
            CreateMap<DataInfo, DataInfoDto>();
            CreateMap<History, HistoryDto>();
            CreateMap<History, HistoryDtoPost>();
            CreateMap<UserHistory, UserHistoryDto>();
            CreateMap<UserHistory, UserHistoryPostDto>();
            

        }
    }
}
