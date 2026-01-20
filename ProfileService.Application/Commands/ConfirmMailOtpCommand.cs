using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record ConfirmMailOtpCommand(string otp,string Email) : IRequest<bool>;

    public class ConfirmMailOtpHandler : IRequestHandler<ConfirmMailOtpCommand,bool>
    {
        private readonly IProfileRepository _profileRepo;
        public ConfirmMailOtpHandler(IProfileRepository profileRepository) { _profileRepo = profileRepository; }
         
        public async Task<bool> Handle(ConfirmMailOtpCommand command,CancellationToken token)
        {
           return await _profileRepo.ConfirmCollege(command.Email, command.otp);
        }

    }
}
