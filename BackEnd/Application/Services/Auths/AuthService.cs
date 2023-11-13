using Application.Auth.Commands;
using Application.Common.Auth;
using Application.Common.Exceptions;
using Application.DTOs.User;
using Application.Interfaces.Auths;
using Application.ViewModel.Auth;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Auths
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _autoMapper;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IMapper autoMapper,
            IAuthRepository authRepository,
            IPasswordHasher passwordHasher
        )
        {
            _authRepository = authRepository;
            _autoMapper = autoMapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> GetUserByLogin(string login)
        {
            return await _authRepository
                    .GetUsers()
                    .Where(c => c.Login == login)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
        }

        public async Task<UserDto> GetUserById(int? Id)
        {
            return await _authRepository
                    .GetUsers()
                    .Where(c => c.Id == Id)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
        }

        public async Task<AuthViewModel> GetAuth(PostLoginCommand auth)
        {
            var userDto = await _authRepository
                    .GetUsers()
                    .Where(x => x.Login.Trim() == auth.Login.Trim())
                    .Include(x => x.Role)
                    .ProjectTo<UserDto>(_autoMapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

            var isValid = false;
            if (userDto != null)
            {
                isValid = _passwordHasher.Check(userDto.Password, auth.Password);
            }
            
            if (!isValid)
            {
                throw new UnAuthorizeException("Usuario o contraseña incorrecta");
            }

            return new AuthViewModel()
            {
                user = userDto,
                token = GetAuthToken(userDto)
            };
        }

        public string GetAuthToken(UserDto user)
        {
            AuthToken auth = new AuthToken() { };
            return auth.GenerateToken(user);
        }
    }
}
