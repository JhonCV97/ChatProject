using Application.Common.Exceptions;
using Application.Common.Response;
using Application.Cqrs.User.Commands;
using Application.DTOs.Email;
using Application.Interfaces.Email;
using Application.Interfaces.SendEmail;
using Application.Interfaces.User;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.SendEmail
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly string _urlPage;

        public SendEmailService(IUserService userService, 
                                IEmailService emailService, 
                                IUnitOfWork unitOfWork, 
                                IMapper mapper,
                                IConfiguration configuration)
        {
            _userService = userService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _autoMapper = mapper;
            _urlPage = configuration["UrlPage"];

        }

        public async Task<ApiResponse<bool>> RecoverPassword(RecoverPasswordCommand request)
        {
            var response = new ApiResponse<bool>();

            try
            {
                var UserExist = _unitOfWork.UserRepository.Get().FirstOrDefault(x => x.Email == request.Email);

                if (UserExist == null)
                {
                    throw new BadRequestException("El correo no esta registrado");
                }

                var email = new EmailDto();

                email.Body = "<html><body>";
                email.Body += "<h2>Cambia la contraseña</h2>";
                email.Body += "<p>Presiona el texto de abajo para cambiar la contraseña</p>";
                email.Body += "<a href='" + _urlPage+ "recoverPassword" + "' target='_blank'>Recuperar Contraseña</a>";
                email.Body += "</body></html>";
                email.To = request.Email;
                email.Subject = "Recuperar Contraseña";
                _emailService.SendEmail(email);

                response.Data = true;
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


