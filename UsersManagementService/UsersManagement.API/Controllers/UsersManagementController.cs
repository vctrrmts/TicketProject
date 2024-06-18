using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.DTOs;
using UsersManagement.Application.Handlers.Command.CreateUser;
using UsersManagement.Application.Handlers.Command.DeleteUser;
using UsersManagement.Application.Handlers.Command.UpdatePassword;
using UsersManagement.Application.Handlers.Query.GetById;
using UsersManagement.Application.Handlers.Query.GetByLogin;
using UsersManagement.Application.Handlers.Query.GetList;

namespace UsersManagement.API.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [Authorize]
    [Route("Users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Commands
        /// <summary>
        /// Create user
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            CreateUserCommand createUserCommand,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var newUser = await mediator.Send(createUserCommand, cancellationToken);
            return Created("/Users/" + newUser.UserId, newUser);
        }

        /// <summary>
        /// Update user's password
        /// </summary>
        [HttpPut("Password")]
        public async Task<IActionResult> UpdatePasswordAsync(
            UpdatePasswordCommand updatePasswordCommand,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(updatePasswordCommand, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
            Guid id,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteUserCommand() { UserId = id }, cancellationToken);
            return Ok();
        }
        #endregion


        #region Queries
        /// <summary>
        /// Get list of users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetListAsync(
            [FromQuery] GetListQuery getListQuery,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var users = await mediator.Send(getListQuery, cancellationToken);
            return Ok(users);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(
            Guid id,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new GetByIdQuery() { UserId = id }, cancellationToken));
        }

        /// <summary>
        /// Get user by login
        /// </summary>
        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> GetByLoginAsync(
            [FromQuery] GetByLoginQuery getByLoginQuery,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(getByLoginQuery, cancellationToken));
        }
        #endregion

    }
}
