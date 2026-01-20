using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Commands;
using ProfileService.Application.Contracts;
using ProfileService.Application.Queries;

namespace ProfileService.API.Controllers
{
    [Authorize]
    [Route("campus-net/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

      
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(ProfileUpdate updatedProflile)
        {
           await _mediator.Send(new UpdateProfileCommand(updatedProflile));
            return Ok();
        }

        
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var profile = await _mediator.Send(new MyProfileQuery());
            return Ok(profile);
        }

     
        [HttpGet("user-profile/{userId}")]
        public async Task<IActionResult> GetUserProfile(string userId)
        {
            var profile = await _mediator.Send(new GetProfileQuery(userId));
            return Ok(profile);
        }


        //Reviews

        [HttpPost("post-review")]
        public async Task<IActionResult> PostReview(ReviewContract review)
        {
             await _mediator.Send(new AddReviewCommand(review));
            return Ok();
        }

        [HttpPost("update-review")]
        public async Task<IActionResult> UpdateReview(string id,int score)
        {
          await _mediator.Send(new UpdateReviewCommand(id,score));
            return Ok();
        }

        [HttpGet("my-review-score")]
        public async Task<IActionResult> MyReview()
        {
            var result = await _mediator.Send(new MyReportScoreQuery());
            return Ok(result);
        }

        [HttpGet("review-by-id/{id}")]
        public async Task<IActionResult> ReviewById(string id)
        {
            var result = await _mediator.Send(new ReviewScoreByReviewedIdQuery(id));
            return Ok(result);
        }


        // Reports
        [HttpPost("post-report")]
        public async Task<IActionResult> PostReport(ReportContract report)
        {
            await _mediator.Send(new PostReportCommand(report));
            return Ok();
        }

        [HttpPost("delete-report/{id}")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _mediator.Send(new  DeleteReportCommand(id));
            return Ok();
        }

        [HttpGet("my-report-score")]
        public async Task<IActionResult> MyReportScore()
        {
            var result = await _mediator.Send(new MyReportScoreQuery());
            return Ok(result);
        }

        [HttpGet("reported-by-me")]
        public async Task<IActionResult> ReportsByMe()
        {
            var result = await _mediator.Send(new AccountsReportedByMeQuery());
            return Ok(result);
        }

       

    }
}
