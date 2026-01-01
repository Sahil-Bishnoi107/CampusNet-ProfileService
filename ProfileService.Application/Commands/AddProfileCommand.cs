using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record AddProfileCommand(string userId, string name, string email, string phoneNo) : IRequest;

    public class AddProfileHandler : IRequestHandler<AddProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        public AddProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            await _profileRepository.AddAsync(request.userId,request.name,request.email,request.phoneNo);
        }

       
    }
}
