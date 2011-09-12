using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Profile.Services.Reviews
{
    public interface IReviewService
    {
        void Save(ProductReview productReview);
        ProductReview GetProductReview(string productId);
    }
}
