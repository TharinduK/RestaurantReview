using System;

namespace RestaurantRating.Domain
{
    public class AddReviewRequestModel : TransactionRequestModel
    {
        public string Comment { get; set; }
        public DateTime DateTimePosted { get; set; }
        public int Rating { get; set; }
        public int RestaurantId { get; set; }

        public override string ToString()
            => $"Rating:{Rating} Restaurant Id:{RestaurantId} Posted:{DateTimePosted} Comment:{Comment}";

        public override bool Equals(object obj)
        {
            var model = obj as AddReviewRequestModel;

            if (model == null) return false;
            else
                return Comment.Equals(model.Comment)
                    && DateTimePosted.Equals(model.DateTimePosted)
                    && Rating.Equals(model.Rating)
                    && RestaurantId.Equals(model.RestaurantId);
        }
        public override int GetHashCode()
        {
            return Comment.GetHashCode() + DateTimePosted.GetHashCode() + Rating.GetHashCode() + RestaurantId.GetHashCode();
        }
    }
}