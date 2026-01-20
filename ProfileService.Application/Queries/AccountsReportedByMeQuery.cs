using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Queries
{
    public record AccountsReportedByMeQuery : IRequest<List<Report>>;

    public class AccountsReportedByMeHandler : IRequestHandler<AccountsReportedByMeQuery, List<Report>>
    {
        private readonly Domain.Interfaces.IReportRepository _reportRepository;
        public AccountsReportedByMeHandler(Domain.Interfaces.IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<List<Report>> Handle(AccountsReportedByMeQuery request, CancellationToken cancellationToken)
        {
          return  await _reportRepository.GetAccountsReportedByMe();
            
        }
    }

}
