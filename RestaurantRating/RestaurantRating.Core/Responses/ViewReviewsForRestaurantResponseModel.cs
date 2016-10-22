using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public class ViewReviewsForRestaurantResponseModel : TransactionResponseModel
    {
        public IEnumerable<Review> Reviews { get; set; }
    }
}