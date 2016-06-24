using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class ReviewResult
    {
        public double AverageReviewScore { get; private set; }

        public IEnumerable<Review> Reviews { get; private set; }
        
        internal ReviewResult(double averageScore, IEnumerable<Review> reviews)
        {
            this.AverageReviewScore = averageScore;
            this.Reviews = reviews;
        } 
    }
}