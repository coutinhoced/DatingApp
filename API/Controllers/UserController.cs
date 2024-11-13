using API.Filters;
using DatingApp.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ServiceFilter(typeof(SanitizeParametersFilter))]
    public class UserController : BaseApiController
    {
        private IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize]
        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery? usersQuery)
        {    
            if(usersQuery == null)
                usersQuery = new GetUsersQuery();
            var response = await mediator.Send(usersQuery);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}
