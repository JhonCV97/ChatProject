using Application.Common.Response;
using Application.Interfaces.DataInfo;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cqrs.DataInfo.Commands
{
    public class AddExcelClassCommand : IRequest<ApiResponse<bool>>
    {
        public IFormFile File { get; set; }
    }
    public class AddExcelClassCommandHandler : IRequestHandler<AddExcelClassCommand, ApiResponse<bool>>
    {
        private readonly IDataInfoService _dataInfoService;
        public AddExcelClassCommandHandler(IDataInfoService dataInfoService)
        {
            _dataInfoService = dataInfoService;
        }

        public async Task<ApiResponse<bool>> Handle(AddExcelClassCommand request, CancellationToken cancellationToken)
        {
            return await _dataInfoService.AddDataInfoClassExcel(request);
        }
    }
}
