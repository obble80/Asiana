using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace Asiana.Profile.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private IMongo mongoService;
        public ReviewService(IMongo mongoService)
        {
            this.mongoService = mongoService;
        }

        public void Save(ProductReview productReview)
        {
            mongoService
                .GetCollection<ProductReview>("productReviews")
                .Save(productReview);
        }

        public ProductReview GetProductReview(string productId)
        {
            return mongoService.GetCollection<ProductReview>("productReviews")
                .AsQueryable()
                .Where(x => x.ProductID == productId)
                .FirstOrDefault();
        }

        public ProductReview GetCustomerReview(string customerId)
        {
            return null;
        }
    }
}
