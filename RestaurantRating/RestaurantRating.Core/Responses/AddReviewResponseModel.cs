namespace RestaurantRating.Domain
{
    public class AddReviewResponseModel:TransactionResponseModel
    {
        public int ReviewNumber { get; set; }

        public override string ToString() => $"Review # {ReviewNumber}";

        public override bool Equals(object obj)
        {
            var model = obj as AddReviewResponseModel;

            if (model == null) return false;
            else
                return ReviewNumber.Equals(model.ReviewNumber);
        }

        public override int GetHashCode() => ReviewNumber.GetHashCode();
    }
}