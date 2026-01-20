using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.DTOs
{
    public class NotificationMessage
    {
        public string To { get; set; }
        public string? Subject { get; set; }   // email only
        public string Message { get; set; }   // OTP text
        public string CorrelationId { get; set; }
    }
}
