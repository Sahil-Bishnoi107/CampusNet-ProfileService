using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record ConfirmPhoneOtpCommand(string otp,string PhoneNo) : IRequest<bool>;

    public class ConfirmPhoneIOtpHandler : IRequestHandler<ConfirmPhoneOtpCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;
        public ConfirmPhoneIOtpHandler (IProfileRepository profileRepository) { _profileRepository = profileRepository;}

        public async Task<bool> Handle(ConfirmPhoneOtpCommand command,CancellationToken token)
        {
            return await _profileRepository.ConfirmPhoneNo(command.PhoneNo,command.otp);
        }
    }
    
}
