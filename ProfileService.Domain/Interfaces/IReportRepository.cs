using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Domain.Entities;

namespace ProfileService.Domain.Interfaces
{
    public interface IReportRepository
    {
        Task PostReport(string reportedId, string reason,string reportedName);
        Task DeleteReport(string reportId);
        Task<int?> GetMyReportedScore();
        Task<List<Report>> GetAccountsReportedByMe();
    }
}
