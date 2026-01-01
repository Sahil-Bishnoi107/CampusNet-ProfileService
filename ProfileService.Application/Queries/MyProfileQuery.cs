using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Entities;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Queries
{
    public record MyProfileQuery : IRequest<Profile>;
    public class MyProfileHandler : IRequestHandler<MyProfileQuery,Profile>
    {
        private readonly IProfileRepository _profileRepository;

        public MyProfileHandler(IProfileRepository profileRepository) { 
            _profileRepository = profileRepository;
        }
        public async Task<Profile> Handle(MyProfileQuery request, CancellationToken cancellationToken)
        {
           return await _profileRepository.GetMyProfile();
        }
    }

}
