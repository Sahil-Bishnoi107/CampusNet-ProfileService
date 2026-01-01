using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Commands;
using ProfileService.Application.Contracts;
using ProfileService.Application.Queries;

namespace ProfileService.API.Controllers
{
    [Route("campus-net/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdate updatedProflile)
        {
           await _mediator.Send(new UpdateProfileCommand(updatedProflile));
            return Ok();
        }

        [Authorize]
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var profile = await _mediator.Send(new MyProfileQuery());
            return Ok(profile);
        }

        [Authorize]
        [HttpGet("user-profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var profile = await _mediator.Send(new GetProfileQuery(userId));
            return Ok(profile);
        }

        [Authorize]
        [HttpGet("debug/claims")]
        public IActionResult DebugClaims()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }

    }
}
