using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Domain.Interfaces;

namespace ProfileService.Application.Queries
{
    public record MyReviewScoreQuery : IRequest<int>;

    public class MyReviewScoreHandler : IRequestHandler<MyReviewScoreQuery,int>
    {
        private readonly IReviewRepository _reviewRepository;
        public MyReviewScoreHandler(IReviewRepository review)
        {
            _reviewRepository = review;
        }

        public async Task<int> Handle(MyReviewScoreQuery query,CancellationToken token)
        {
            return await _reviewRepository.GetMyReviewScore() ?? 0;
        }
    }
    
}
