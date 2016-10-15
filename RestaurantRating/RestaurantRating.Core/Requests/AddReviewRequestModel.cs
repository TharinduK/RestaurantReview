using System;

namespace RestaurantRating.Domain
{
    public class AddReviewRequestModel : TransactionRequestModel
    {
        public string Comment { get; set; }
        public DateTime DateTimePosted { get; set; }
        public int Rating { get; set; }
        public int RestaruntId { get; set; }

        public override string ToString()
            => $"Rating:{Rating} Restaurant Id:{RestaruntId} Posted:{DateTimePosted} Comment:{Comment}";

    }
}