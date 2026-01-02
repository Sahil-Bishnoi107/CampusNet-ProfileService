using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using ProfileService.Domain.Interfaces;
using ProfileService.Infrastructure.Persistence;

namespace ProfileService.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly IJwtRepository _jwtRepository;
        public ProfileRepository(AppDbContext context, IJwtRepository jwtRepository)
        {
            _context = context;
            _jwtRepository = jwtRepository;
        }
        public async Task AddProfileAsync(string userId,string name,string email,string phoneNo)
        {
            Profile profile = new Profile(userId,email, name, phoneNo);
           await  _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

        }

        public async Task<Profile> GetByIdAsync(string id)
        {
            Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
            if(profile == null)
            {
                throw new Exception("Profile not found");
            }
            return profile;
        }

        public async Task<Profile> GetMyProfile()
        {
            string userId = _jwtRepository.GenerateUserId();
            Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == userId);
            if (profile == null)
            {
                throw new Exception("Unexpected error. Log In Again");
            }
            return profile;
        }

        public async Task UpdateAsync(string? username, string? bio, string? rollno, string? collegeEmail, string? githubLink, string? linkedinLink, string? profilePicLink, string? branch, string? degree)
        {
           string userId = _jwtRepository.GenerateUserId();
            Profile? profile = await _context.Profiles.FirstOrDefaultAsync(p => p.Id == userId);
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            profile.UpdateProfile(username, bio, rollno, collegeEmail, githubLink, linkedinLink, profilePicLink, branch, degree);
            _context.Profiles.Update(profile);
           await _context.SaveChangesAsync();

        }
    }
}
