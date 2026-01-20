using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Entities
{
    public class ProfileOtps
    {
        public string Id { get;private set; }

        public string Address { get;private set; }

        public string UserId { get;private set; }
        public string Type { get; private set; }
        public string Otp { get; private set; }

        public DateTime ExpiresAt { get; private set; }
        public bool Status { get; private set; }

        public ProfileOtps()
        {
        }

        public ProfileOtps(string userId, string type, string otp, DateTime expiresAt,string address)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Type = type;
            Otp = otp;
            ExpiresAt = expiresAt;
            Status = false;
            Address = address;
        }
        public void MarkAsUsed()
        {
            Status = true;
        }

    }
}
