using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using ProfileService.Domain.Interfaces;
using ProfileService.Infrastructure.Persistence;
using static System.Net.WebRequestMethods;

namespace ProfileService.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly IJwtRepository _jwtRepository;
        private readonly ISmsRepository _smsRepository;
        public ProfileRepository(AppDbContext context, IJwtRepository jwtRepository, ISmsRepository smsRepository)
        {
            _context = context;
            _jwtRepository = jwtRepository;
            _smsRepository = smsRepository;
        }
        public async Task AddProfileAsync(string userId,string name,string email,string phoneNo)
        {
            Profile profile = new Profile(userId,email, name, phoneNo);
           await  _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ConfirmCollege(string email, string otp)
        {
            string id = _jwtRepository.GenerateUserId();
            var result = await _context.ProfileOtps.Where(p => p.UserId == id && p.Type == "Mail" && p.Address == email).FirstOrDefaultAsync();
            
            if (result == null)
            {
                throw new Exception("No record found");
            }
           
            if(result.Otp != otp) { throw new Exception("Invalid Otp"); }
            if (result.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("OTP Expired");
            }
            if (result.Status == true)
            {
                throw new Exception("OTP Already Used");
            }
            result.MarkAsUsed();
            _context.ProfileOtps.Update(result);
            var profile = await _context.Profiles.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            profile.UpdateCollegeEmail(result.Address);
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> ConfirmPhoneNo(string phoneNo, string otp)
        {
            string id = _jwtRepository.GenerateUserId();
            var result = await _context.ProfileOtps.Where(p => p.UserId == id  && p.Type == "Phone" && p.Address == phoneNo).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("No records found");
            }
            
            if (result.Otp != otp) { throw new Exception("Invalid Otp"); }
            if (result.ExpiresAt < DateTime.UtcNow)
            {
                throw new Exception("OTP Expired");
            }
            if (result.Status == true)
            {
                throw new Exception("OTP Already Used");
            }
            result.MarkAsUsed();
            _context.ProfileOtps.Update(result);
            var profile = await _context.Profiles.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            profile.UpdatePhoneNo(result.Address);

            await _context.SaveChangesAsync();
            return true;
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
