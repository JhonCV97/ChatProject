using Application.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ChatProject.Controllers
{
    [Route("api/auth-token")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Auth-Token")]
    public class AuthTokenController : ApiControllerBase
    {
        /// <summary>
        /// Método que se encarga de generar el token de acceso.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] PostLoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
