using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Contracts;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record UpdateProfileCommand(ProfileUpdate updatedProfile) : IRequest;
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        public UpdateProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            ProfileUpdate p = request.updatedProfile;
            await _profileRepository.UpdateAsync(p.userName,p.bio,p.rollNumber,p.collegeEmail,p.gitHubLink,p.linkedInLink,p.profilePictureUrl,p.branch,p.degree);
        }
    }

}
