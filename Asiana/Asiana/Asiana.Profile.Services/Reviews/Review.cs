using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Profile.Services.Reviews
{
    public class Review
    {
        public string UserID { get; set; }
        public string UserNickname { get; set; }
        public string ShortComment { get; set; }
        public string FullComment { get; set; }
        public double ValueRating { get; set; }
        public double QualityRating { get; set; }
        public double DesignRating { get; set; }
        public DateTime Created { get; set; }
        public bool InAppropriate { get; set; }

        private List<String> pros = new List<string>();
        private List<String> cons = new List<string>();

    }
}
