using Application.Cqrs.DataInfo.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.ChatProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Data-Info")]
    public class DataInfoController : ApiControllerBase
    {
        [HttpPost("AddExcelClass")]
        public async Task<IActionResult> AddExcelClass([FromForm] AddExcelClassCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
