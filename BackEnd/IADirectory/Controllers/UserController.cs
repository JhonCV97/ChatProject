using Application.Cqrs.User.Commands;
using Application.Cqrs.User.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.ChatProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Users")]
    //[Authorize]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Agrega un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] PostUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Trae todos los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        /// <summary>
        /// Actualiza los Usuarios
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] PutUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Eliminar Usuarios
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand() { Id = Id }));
        }

        /// <summary>
        /// Recuperar Contraseña por Correo Electronico
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Obtener Usuario por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            return Ok(await Mediator.Send(new GetUsersQueryById() { Id = Id }));
        }
        
    }
}
