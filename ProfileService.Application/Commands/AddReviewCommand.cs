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
    public record AddReviewCommand(ReviewContract contract) : IRequest;

    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand>
    {
        private readonly IReviewRepository _review;
        public AddReviewCommandHandler(IReviewRepository review) { _review = review; }

        public async Task Handle(AddReviewCommand r, CancellationToken cancellationToken)
        {
            await _review.PostReview(r.contract.ReviewedId,r.contract.ReviewScore, r.contract.ReviewedName);
        }

    }
    
}
