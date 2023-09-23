using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Application.DTOs.User;
using Application.Interfaces.User;
using AutoMapper;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUnitOfWork unitOfWork, IMapper autoMapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<List<UserDto>>> GetUsers()
        {
            var response = new ApiResponse<List<UserDto>>();

            try
            {
                response.Data = _autoMapper.Map<List<UserDto>>(await _unitOfWork.UserRepository.Get().ToListAsync());
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. { ex.Message } ";
            }

            return response;
        }

        public async Task<ApiResponse<UserDto>> GetUsersById(GetUsersQueryById request)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.GetById(request.Id));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al consultar el registro, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }


        public async Task<ApiResponse<UserDto>> AddUser(PostUserCommand request)
        {
            var response = new ApiResponse<UserDto>();

            try
            {
                var ExitsUser = await _unitOfWork.UserRepository.Get()
                                                                .Where(x => x.Login == request.UserPostDto.Login)
                                                                .FirstOrDefaultAsync();
                if (ExitsUser != null)
                {
                    throw new BadRequestException("El correo ya esta creado, por favor recupera la contraseña");
                }

                var User = _autoMapper.Map<Domain.Models.User>(request.UserPostDto);
                User.Password = _passwordHasher.Hash(User.Password);

                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.Add(User));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al Crear Usuario. { ex.Message } ";
            }

            return response;
        }

        public async Task<ApiResponse<UserDto>> UpdateUser(PutUserCommand request)
        {
            var response = new ApiResponse<UserDto>();
            try
            {
                var ExitsUser = await _unitOfWork.UserRepository.Get()
                                                                .Where(x => x.Login == request.UserDto.Email)
                                                                .AsNoTracking()
                                                                .FirstOrDefaultAsync();

                if (ExitsUser == null)
                {
                    throw new BadRequestException("El correo no esta registrado verifique por favor");
                }

                if (request.UserDto.Id == 0)
                {
                    request.UserDto.Id = ExitsUser.Id;
                }

                if (request.UserDto.FullName == "")
                {
                    request.UserDto.FullName = ExitsUser.FullName;
                }

                if (request.UserDto.RoleId == 0)
                {
                    request.UserDto.RoleId = ExitsUser.RoleId;
                }


                request.UserDto.Password = _passwordHasher.Hash(request.UserDto.Password);
                var userDto = new UserDto
                {
                    Id = request.UserDto.Id,
                    Email = request.UserDto.Email,
                    FullName = request.UserDto.FullName, 
                    Password= request.UserDto.Password,
                    Login= request.UserDto.Login,
                    RoleId= request.UserDto.RoleId
                };

                response.Data = _autoMapper.Map<UserDto>(await _unitOfWork.UserRepository.Put(_autoMapper.Map<Domain.Models.User>(userDto)));
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al actualizar el registro, consulte con el administrador. {ex.Message} ";
                throw;
            }
            return response;
        }

        public async Task<ApiResponse<bool>> DeleteUser(DeleteUserCommand request)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var CategoriesAI = await _unitOfWork.UserRepository.GetById(request.Id);

                response.Data = await _unitOfWork.UserRepository.Delete(CategoriesAI);
                response.Result = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al eliminar el registro, consulte con el administrador. {ex.Message} ";
                throw;
            }
            return response;
        }
        
    }
}
