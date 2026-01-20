using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Contracts;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Commands
{
    public record PostReportCommand(ReportContract report) : IRequest;

    public class PostReportHandler : IRequestHandler<PostReportCommand>
    {
        private readonly IReportRepository _reportRepository;

        public PostReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task Handle(PostReportCommand request, CancellationToken cancellationToken)
        {
            await _reportRepository.PostReport(request.report.ReportedId,request.report.Reason,request.report.ReportedName);
        }
    }
}
