using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace Asiana.Profile.Services.Reviews
{
    public class ProductReview
    {
        private List<Review> reviews = new List<Review>();
        public String ProductID { get; set; }
        public String ProductTitle { get; set; }
        public ObjectId ID { get; set; }

        public double QualityRating {
            get
            {
                if (reviews.Count > 0)
                {
                    return reviews.Average(x => x.QualityRating);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double ValueRating {
            get
            {
                if (reviews.Count > 0)
                {
                    return reviews.Average(x => x.ValueRating);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double DesignRating
        {
            get
            {
                if (reviews.Count > 0)
                {
                    return reviews.Average(x => x.DesignRating);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double Rating
        {
            get
            {
                return (this.ValueRating + this.DesignRating + this.QualityRating) / 3;
            }
        }
     
        public List<Review> Reviews
        {
            get { return reviews; }
            set { reviews = value; }
        }

    }
}
