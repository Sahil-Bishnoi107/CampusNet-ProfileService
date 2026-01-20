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
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        private readonly IJwtRepository _jwtRepository;
        public ReviewRepository(AppDbContext context, IJwtRepository jwtRepository)
        {
            _context = context;
            _jwtRepository = jwtRepository;
        }
        public async Task<int?> GetMyReviewScore()
        {
            var id = _jwtRepository.GenerateUserId();
            var reviews = await _context.Reviews.Where(r => r.reviewedId == id).ToListAsync();
            if(reviews.Count == 0)
            {
                return 0;
            }
            return (int?)reviews.Average(r => r.reviewScore);
        }

        public async Task<int?> GetReviewScoreByReviewedId(string reviewedId)
        {
           var reviews = await _context.Reviews.Where(r => r.reviewedId == reviewedId).ToListAsync();
            if (reviews.Count == 0)
            {
                return 0;
            }
            return (int?)reviews.Average(r => r.reviewScore);
        }

        public async Task PostReview(string reviewedId, int reviewScore,string reviewedName)
        {
            Review review = new Review(_jwtRepository.GenerateUserId(), reviewedId, reviewScore, reviewedName);
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReview(string reviewId, int reviewScore)
        {
            Review? review = await _context.Reviews.Where(x => x.id == reviewId).FirstOrDefaultAsync();   
            if(review == null) { throw new Exception("Review not found"); }
            review.UpdateReview(reviewScore);
            await _context.SaveChangesAsync();
        }
    }
}
