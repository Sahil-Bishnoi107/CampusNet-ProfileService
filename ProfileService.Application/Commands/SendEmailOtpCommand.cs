using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Commands
{
    public record SendEmailOtpCommand(string email) : IRequest;

    public class SendEmailOtpHandler : IRequestHandler<SendEmailOtpCommand>
    {
        private readonly Domain.Interfaces.ISmsRepository _smsRepository;
        public SendEmailOtpHandler(Domain.Interfaces.ISmsRepository smsRepository)
        {
            _smsRepository = smsRepository;
        }
        public async Task Handle(SendEmailOtpCommand request, CancellationToken cancellationToken)
        {
            await _smsRepository.SendOtpOnMail(request.email);
          
        }
    }


}
