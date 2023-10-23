using Application.Cqrs.History.Commands;
using Application.Cqrs.History.Queries;
using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.ChatProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "History")]
    public class HistoryController : ApiControllerBase
    {
        /// <summary>
        /// Trae todos los usuarios de la base de datos
        /// </summary>
        /// /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHistoryBySession([FromQuery] GetHistoryQueryBySession query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Agrega un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostHistoryCommand([FromBody] PostHistoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
