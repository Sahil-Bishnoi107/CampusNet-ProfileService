using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Entities
{
    public class Review
    {
        public string id { get; private set; }
        public string reviewerId { get; private set; }

        public string reviewedName { get; private set; }
        public string reviewedId { get; private set; }
        public int reviewScore { get; private set; }

        Review() { }

        public Review(string reviewerId, string reviewedId, int reviewScore,string reviewedName)
        {
            this.id = Guid.NewGuid().ToString();
            this.reviewerId = reviewerId;
            this.reviewedId = reviewedId;
            this.reviewScore = reviewScore;
            this.reviewedName = reviewedName;
        }
        public int UpdateReview(int reviewScore) => this.reviewScore = reviewScore;
    }
}
