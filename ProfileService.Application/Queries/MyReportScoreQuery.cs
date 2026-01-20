using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Queries
{
    public record MyReportScoreQuery : IRequest<int>;

    public class MyReportScoreHandler : IRequestHandler<MyReportScoreQuery, int>
    {
        private readonly IReportRepository _reportRepository;

        public MyReportScoreHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
         public async Task<int> Handle(MyReportScoreQuery query, CancellationToken token)
        {
           return await _reportRepository.GetMyReportedScore() ?? 0;
        }

    }
}
