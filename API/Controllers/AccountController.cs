using DatingApp.Application.Features.User.Commands;
using DatingApp.Application.Features.User.Queries;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("register")] //api/account/register
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterUsersCommand command)
        {
            if (string.IsNullOrEmpty(command.username) || string.IsNullOrEmpty(command.password))
            {
                return BadRequest("Username and Password is mandatory");
            }

            UserDto response = await mediator.Send(command);

            if (response.Exception != null)
            {
                return BadRequest(response.Exception.Message);
            }
            return Ok(response);
        }

        [HttpPost("login")] //api/account/login
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginUserQuery loginUserQuery)
        {
            UserDto response = await mediator.Send(loginUserQuery);
            if (!string.IsNullOrEmpty(response.ValidationError))
            {
                return Unauthorized(response.ValidationError);
            }
            return Ok(response);
        }

    }
}
