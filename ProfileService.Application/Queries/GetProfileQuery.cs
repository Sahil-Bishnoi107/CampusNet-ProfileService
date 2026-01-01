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
    public record GetProfileQuery(string userId) : IRequest<Profile>;

    public class GetProfileHandler : IRequestHandler<GetProfileQuery,Profile>
    {
        private readonly IProfileRepository _profileRepository;

        public GetProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<Profile> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            return await _profileRepository.GetByIdAsync(request.userId);
        }
    
        
    }

}
