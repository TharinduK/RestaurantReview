namespace RestaurantRating.Domain
{
    public class AddReviewResponseModel:TransactionResponseModel
    {
        public int ReviewNumber { get; set; }

        public override string ToString() => $"Review # {ReviewNumber}";
    }
}