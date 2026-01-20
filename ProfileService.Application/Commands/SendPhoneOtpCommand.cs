using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record SendPhoneOtpCommand(string phone) : IRequest;

    public class SendPhoneOtpHandler : IRequestHandler<SendPhoneOtpCommand> 
    {
        private readonly ISmsRepository _smsRepository;
        public SendPhoneOtpHandler(ISmsRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }
        public async Task Handle(SendPhoneOtpCommand request, CancellationToken cancellationToken)
        {
            await _smsRepository.SendOtpOnPhone(request.phone);
        }

    }
}
