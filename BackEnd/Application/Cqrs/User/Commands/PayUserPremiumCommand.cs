using Application.Common.Response;
using Application.Interfaces.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.User.Commands
{
    public class PayUserPremiumCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
    }
    public class PayUserPremiumCommandHandler : IRequestHandler<PayUserPremiumCommand, ApiResponse<bool>>
    {
        private readonly IUserService _userService;
        public PayUserPremiumCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<bool>> Handle(PayUserPremiumCommand request, CancellationToken cancellationToken)
        {
            return await _userService.PayUserPremium(request);
        }
    }
}
