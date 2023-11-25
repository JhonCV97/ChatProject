using Application.Cqrs.ConnectionChatGPT.Queries;
using Application.Cqrs.History.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.ChatProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ConnectionChatGPT")]
    //[Authorize]
    public class ConnectionChatGPTController : ApiControllerBase
    {
        /// <summary>
        /// Conexion a chatGPT
        /// </summary>
        /// /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMessageChatGPT([FromQuery] GetMessageChatGPTQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
