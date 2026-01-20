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
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;
        private readonly IJwtRepository _jwtRepository;
        public ReportRepository(AppDbContext context,IJwtRepository jwtRepository) {
            _context = context;
            _jwtRepository = jwtRepository;
        }
        public async Task DeleteReport(string reportId)
        {
            Report? report = await _context.Reports.Where(r => r.id == reportId).FirstOrDefaultAsync();
            if(report == null) { throw new Exception("Report not found"); }
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Report>> GetAccountsReportedByMe()
        {
            string id = _jwtRepository.GenerateUserId();
            var reports = await _context.Reports.Where(x => x.reporterId == id).ToListAsync();
            return reports;
        }

        public async Task<int?> GetMyReportedScore()
        {
            string id = _jwtRepository.GenerateUserId();
            var reports = await _context.Reports.Where(x => x.reportedId == id).ToListAsync();
            return reports.Count;
        }

        public async Task PostReport(string reportedId, string reason, string reportedName)
        {
            string id = _jwtRepository.GenerateUserId();
            Report report = new Report(id, reportedId, reason, reportedName);
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

        }
    }
}
