using API.Filters;
using DatingApp.Application.Features.User.Commands;
using DatingApp.Application.Features.User.Queries;
using DatingApp.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{

    [ServiceFilter(typeof(SanitizeParametersFilter))]
    [Authorize]
    public class UserController : BaseApiController
    {
        private IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery? usersQuery)
        {
            if (usersQuery == null)
                usersQuery = new GetUsersQuery();
            var response = await mediator.Send(usersQuery);

            if (response == null) return NotFound();

            return Ok(response);
        }


        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand user)
        {            
            await mediator.Send(user);
            return NoContent();
        }

        [HttpPost("Add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file, [FromForm] int UserId)
        {
            AddPhotoCommand command = new AddPhotoCommand();
            command.file = file;
            command.UserId = UserId;
            PhotoDto response =  await mediator.Send(command);

            if (!string.IsNullOrEmpty(response.ValidationError))
            {
                return BadRequest(response.ValidationError);
            }
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return CreatedAtAction(nameof(GetUsers), new GetUsersQuery { name = currentUser }, response);
        }
    }
}
