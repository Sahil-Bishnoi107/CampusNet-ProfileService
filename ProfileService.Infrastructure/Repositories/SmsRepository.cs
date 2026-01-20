using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Domain.Entities;
using ProfileService.Domain.Interfaces;
using ProfileService.Infrastructure.Persistence;

namespace ProfileService.Infrastructure.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        private readonly IJwtRepository _jwtRepository;
        private readonly AppDbContext _context;
        public SmsRepository(IJwtRepository jwtRepository, AppDbContext context) {
            _jwtRepository = jwtRepository;
            _context = context;
        }

        public async Task SendOtpOnMail(string email)
        {
            
            DateTime date = DateTime.UtcNow.AddMinutes(15);
            string id = _jwtRepository.GenerateUserId();
            string otp = Random.Shared.Next(100000, 999999).ToString();
            ProfileOtps profileOtps = new ProfileOtps(id, "Mail", otp, date,email);
            _context.ProfileOtps.Add(profileOtps);
            await _context.SaveChangesAsync();

            // Implemet the sms servfice here

        }

        public async Task SendOtpOnPhone(string phoneNo)
        {
            
            DateTime date = DateTime.UtcNow.AddMinutes(15);
            string id = _jwtRepository.GenerateUserId();
            string otp = Random.Shared.Next(100000, 999999).ToString();
            ProfileOtps profileOtps = new ProfileOtps(id, "Phone", otp, date, phoneNo);
            _context.ProfileOtps.Add(profileOtps);
            await _context.SaveChangesAsync();

            // Implemet the sms servfice here
        }
    }
}
