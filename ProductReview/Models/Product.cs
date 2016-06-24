using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductReview.Models
{
    public sealed class Product
    {
        public string Id { get; private set; }

        public string Name { get; set; }

        public ReviewResult Reviews
        {
            get
            {
                return new ReviewResult(this._averageRatingScore, this._reviews);
            }
        }

        public Product(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.syncObject = new object();
            this._reviews = new List<Review>();
            this._cumulativeReviewScore = 0;
            this._cumulativeReviewCount = 0;
        } 

        public void AddReview(Review review)
        {
            lock(this.syncObject)
            {
                if (this._reviews.Any(r => r.ReviewerId == review.ReviewerId))
                {
                    throw new ReviewExistForUserException(
                        string.Format("User with Id {0} has already submitted a review for product with Id {1}", review.ReviewerId, this.Id));
                }

                this._reviews.Add(review);
                this._cumulativeReviewScore += review.RatingScore;
                this._cumulativeReviewCount++;
                this._averageRatingScore = Math.Round((double)(this._cumulativeReviewScore / this._cumulativeReviewCount), 1);
            }
        }

        private object syncObject;

        private List<Review> _reviews;

        private ulong _cumulativeReviewScore;

        private uint _cumulativeReviewCount;

        private double _averageRatingScore;
    }
}