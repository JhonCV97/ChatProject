using Application.Common.Response;
using Application.Cqrs.User.Commands;
using System.Threading.Tasks;

namespace Application.Interfaces.SendEmail
{
    public interface ISendEmailService
    {
        Task<ApiResponse<bool>> RecoverPassword(RecoverPasswordCommand request);
    }
}