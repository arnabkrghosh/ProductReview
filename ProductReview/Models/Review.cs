using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class Review
    {
        public ushort RatingScore { get; set; }

        public string Comment { get; set; }

        public string ReviewerId { get; private set; }
        
        public Review(string reviewerId, ushort ratingScore, string comment)
        {
            this.ReviewerId = reviewerId;
            if (!this.IsRatingScoreValid(ratingScore))
            {
                throw new InvalidRatingScoreException(
                    string.Format("Rating score {0} falls outside the bounds of acceptable rating score between {1} and {2}",
                    ratingScore,
                    MinRatingScore,
                    MaxRatingScore));
            }

            this.RatingScore = ratingScore;
            this.Comment = comment;
        }

        private const ushort MinRatingScore = 0;

        private const ushort MaxRatingScore = 10;

        private bool IsRatingScoreValid(ushort ratingScore)
        {
            return (ratingScore >= MinRatingScore && ratingScore <= MaxRatingScore);
        }
    }
}