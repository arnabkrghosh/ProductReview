using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductReview.Controllers;
using System.Net;
using System.Web.Http;
using ProductReview.Models;
using System.Collections.Generic;

namespace ProductReview.Tests
{
    [TestClass]
    public class ReviewControllerTests
    {
        [TestMethod]
        public void AddReviewTest()
        {
            ReviewController controller = this.InitializeController();

            string productId = ProductAndUserRepo.ProductId1;
            string userId = ProductAndUserRepo.UserId1;

            HttpResponseMessage response = controller.AddReview(productId, userId, 7, "Absolutely love this product");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void GetReviewsTest()
        {
            ReviewController controller = this.InitializeController();

            string productId = ProductAndUserRepo.ProductId2;
            string userId = ProductAndUserRepo.UserId1;
            HttpResponseMessage response = controller.AddReview(productId, userId, 9, "Absolutely love this product");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            userId = ProductAndUserRepo.UserId2;
            response = controller.AddReview(productId, userId, 1, "Absolutely hate this product");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            ReviewResult result = controller.GetReviews(productId);
            Assert.AreEqual(5, result.AverageReviewScore);
            Assert.AreEqual(2, new List<Review>(result.Reviews).Count);
        }

        [TestMethod]
        public void AddReviewShouldFailForNonExistingProduct()
        {
            ReviewController controller = this.InitializeController();

            string productId = "non-existing";
            string userId = ProductAndUserRepo.UserId1;
            HttpResponseMessage response = controller.AddReview(productId, userId, 9, "Absolutely love this product");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void AddReviewShouldFailForNonExistingUser()
        {
            ReviewController controller = this.InitializeController();

            string productId = ProductAndUserRepo.ProductId2;
            string userId = "non-existing";
            HttpResponseMessage response = controller.AddReview(productId, userId, 9, "Absolutely love this product");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public void AddReviewShouldFailForMultipleReviewsFromSameUser()
        {
            ReviewController controller = this.InitializeController();

            string productId = ProductAndUserRepo.ProductId3;
            string userId = ProductAndUserRepo.UserId1;
            HttpResponseMessage response = controller.AddReview(productId, userId, 9, "Absolutely love this product");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            response = controller.AddReview(productId, userId, 1, "Absolutely hate this product");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private ReviewController InitializeController()
        {
            var controller = new ReviewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller;
        }
    }
}
