using Application.DTOs.DataInfo;
using Application.DTOs.History;
using Application.DTOs.Role;
using Application.DTOs.User;
using Application.DTOs.UserHistory;
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
            CreateMap<DataInfoDto, DataInfo>();
            CreateMap<DataInfoPostDto, DataInfo>();
            CreateMap<HistoryDto, History>();
            CreateMap<HistoryDtoPost, History>();
            CreateMap<UserHistoryDto, UserHistory>();
            CreateMap<UserHistoryPostDto, UserHistory>();
            
            
        }
    }
}
