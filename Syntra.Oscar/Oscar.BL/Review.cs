using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oscar.BL
{
    public class Review
    {
        private Nullable<Guid> reviewId;
        private string reviewContent;
        private int reviewScore;
        private Nullable<Guid> userId;

        public Nullable<Guid> ReviewId
        {
            get { return reviewId; }
            set
            {
                if (reviewId == null)
                {
                    reviewId = value;
                }
            }
        }

        public string ReviewContent
        {
            get { return reviewContent; }
            set { reviewContent = value; }
        }

        public int ReviewScore
        {
            get { return reviewScore; }
            set { reviewScore = value; }
        }

        public Nullable<Guid> UserId
        {
            get { return userId; }
            set
            {
                if (userId == null)
                {
                    userId = value;
                }
            }
        }
    }
}
