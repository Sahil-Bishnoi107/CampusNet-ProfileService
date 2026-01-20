using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Domain.DTOs;

namespace ProfileService.Domain.Interfaces
{
    public interface INotificationPublisher
    {
        Task PublishSmsAsync(NotificationMessage message);
        Task PublishEmailAsync(NotificationMessage message);
    }
}
