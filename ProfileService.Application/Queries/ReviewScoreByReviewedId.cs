using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Queries
{
    public record ReviewScoreByReviewedIdQuery(string reviewedId) : IRequest<int>;

    public class ReviewScoreByReviwedIdHandler : IRequestHandler<ReviewScoreByReviewedIdQuery, int>
    {
        private readonly IReviewRepository _review;
        public ReviewScoreByReviwedIdHandler(IReviewRepository review)
        {
            _review = review;
        }

        public async Task<int> Handle(ReviewScoreByReviewedIdQuery request,CancellationToken token)
        {
            return await _review.GetReviewScoreByReviewedId(request.reviewedId) ?? 0;
        }

    }
}
