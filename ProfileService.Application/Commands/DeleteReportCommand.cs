using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Commands
{
    public record DeleteReportCommand(string id) : IRequest;

    public class DeleteReportHandler : IRequestHandler<DeleteReportCommand>
    {
        private readonly Domain.Interfaces.IReportRepository _reportRepository;
        public DeleteReportHandler(Domain.Interfaces.IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            await _reportRepository.DeleteReport(request.id);
        }
    }

}
