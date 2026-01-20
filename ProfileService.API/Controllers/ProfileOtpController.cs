using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Commands;

namespace ProfileService.API.Controllers
{
    [Authorize]
    [Route("campus-net/otps")]
    [ApiController]
    public class ProfileOtpController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfileOtpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-email-otp")] 
        public async Task<IActionResult> SendEmailOtp(string email)
        {
            await _mediator.Send(new Application.Commands.SendEmailOtpCommand(email));
            return Ok();
        }

        [HttpPost("verify-email-otp")]
        public async Task<IActionResult> VerifyEmailOtp( string otp,string Email)
        {
            var isValid = await _mediator.Send(new ConfirmMailOtpCommand(otp,Email));
            return Ok(isValid);
        }

        [HttpPost("send-phone-otp")]
        public async Task<IActionResult> SendPhoneOtp(string phoneNumber)
        {
            await _mediator.Send(new Application.Commands.SendPhoneOtpCommand(phoneNumber));
            return Ok();
        }

        [HttpPost("verify-phone-otp")]
        public async Task<IActionResult> VerifyPhoneOtp(string phoneNumber, string otp)
        {
            var isValid = await _mediator.Send(new ConfirmPhoneOtpCommand(otp,phoneNumber));
            return Ok(isValid);
        }

    }
}
