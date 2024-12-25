using API.Filters;
using DatingApp.Application.Features.User.Commands;
using DatingApp.Application.Features.User.Queries;
using DatingApp.Domain.Entities;
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

        [HttpPost("get-users")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery? usersQuery)
        {
            if (usersQuery == null)
                usersQuery = new GetUsersQuery();
            var response = await mediator.Send(usersQuery);

            if (response == null) return NotFound();

            return Ok(response);
        }


        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand user)
        {            
            await mediator.Send(user);
            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<Photo>> AddPhoto(IFormFile file, [FromForm] int UserId)
        {
            AddPhotoCommand command = new AddPhotoCommand();
            command.file = file;
            command.UserId = UserId;
            Photo response =  await mediator.Send(command);

            if (!string.IsNullOrEmpty(response.ValidationError))
            {
                return BadRequest(response.ValidationError);
            }
            var currentUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return CreatedAtAction(nameof(GetUsers), new GetUsersQuery { name = currentUser }, response);
        }


        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            SetMainPhotoCommand command = new SetMainPhotoCommand();
            command.photoId = photoId;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("delete-photo")]
        public async Task<ActionResult> DeletePhoto([FromBody] DeletePhotoCommand user)
        {
           var result = await mediator.Send(user);
           return NoContent();
        }
    }
}
