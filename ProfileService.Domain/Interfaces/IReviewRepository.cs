using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Interfaces
{
    public interface IReviewRepository
    {
        Task PostReview(string reviewedId,int reviewScore,string reviewedName);
        Task<int?> GetReviewScoreByReviewedId(string reviewedId);
        Task<int?> GetMyReviewScore();
        Task UpdateReview(string reviewId, int reviewScore);
    }
}
