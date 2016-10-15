namespace RestaurantRating.Domain
{
    public class RemoveRestaurantRequestModel : TransactionRequestModel
    {
        public int RestaurantId { get; set; }
    }
}