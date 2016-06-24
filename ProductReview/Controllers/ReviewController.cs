using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProductReview.Models;

namespace ProductReview.Controllers
{
    public sealed class ReviewController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage AddReview(string productId, string userId, ushort ratingScore, string comment)
        {
            Product product;
            if (!ProductAndUserRepo.ProductList.TryGetValue(productId, out product))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(string.Format("Product with Id {0} not in Product List", productId)));
            }

            User user;
            if (!ProductAndUserRepo.UserList.TryGetValue(userId, out user))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(string.Format("User with Id {0} not in User List", userId)));
            }

            Review review = null;
            try
            {
                review = new Review(userId, ratingScore, comment);
            }
            catch (InvalidRatingScoreException invalidScoreException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(invalidScoreException.Message));
            }

            try
            {
                product.AddReview(review);
            }
            catch (ReviewExistForUserException reviewExistException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(reviewExistException.Message));
            }

            return Request.CreateResponse<Review>(HttpStatusCode.Created, review);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddReviewAsync(string productId, string userId, ushort ratingScore, string comment)
        {
            return await Task.FromResult(AddReview(productId, userId, ratingScore, comment));
        }

        public ReviewResult GetReviews(string productId)
        {
            Product product;
            if (!ProductAndUserRepo.ProductList.TryGetValue(productId, out product))
            {
                throw new HttpResponseException(
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError(string.Format("Product with Id {0} not in Product List", productId))));
            }

            return product.Reviews;
        }

        public async Task<ReviewResult> GetReviewsAsync(string productId)
        {
            return await Task.FromResult(GetReviews(productId));
        }
    }
}
