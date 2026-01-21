using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.Domain.Entities;
using ProfileService.Domain.Interfaces;
using ProfileService.Infrastructure.Persistence;

namespace ProfileService.Infrastructure.Repositories
{
    public class SmsRepository : ISmsRepository
    {
        private readonly IJwtRepository _jwtRepository;
        private readonly AppDbContext _context;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly ILogger<SmsRepository> _logger;
        public SmsRepository(IJwtRepository jwtRepository, AppDbContext context, INotificationPublisher notificationPublisher, ILogger<SmsRepository> logger)
        {
            _jwtRepository = jwtRepository;
            _context = context;
            _notificationPublisher = notificationPublisher;
            _logger = logger;
        }

        public async Task SendOtpOnMail(string email)
        {
            var exists = await _context.ProfileOtps.Where(x => x.Address == email && x.Type == "Mail").FirstOrDefaultAsync();
            
            DateTime date = DateTime.UtcNow.AddMinutes(15);
            string id = _jwtRepository.GenerateUserId();
            string otp = Random.Shared.Next(100000, 999999).ToString();
            ProfileOtps profileOtps = new ProfileOtps(id, "Mail", otp, date,email);
            if (exists == null)
            {
                _context.ProfileOtps.Add(profileOtps);
            }
            else
            {
                exists.UpdateOtp(otp);
                _context.ProfileOtps.Update(exists);
            }
            await _context.SaveChangesAsync();

            // Implemet the sms servfice here
          await  _notificationPublisher.PublishEmailAsync(new Domain.DTOs.NotificationMessage
          {
              To = email,
              Subject = "Your OTP Code",
              Message = $"Your OTP code is {otp}. It will expire in 15 minutes."
          });
            _logger.LogInformation("CollegeEmail OTP sent on {email}",email);

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
            await _notificationPublisher.PublishSmsAsync(new Domain.DTOs.NotificationMessage
            {
                To = phoneNo,
                Message = $"Your OTP code is {otp}. It will expire in 15 minutes."
            });
            _logger.LogInformation("PhoneNo OTP sent on {phoneNo}", phoneNo);
        }
    }
}
