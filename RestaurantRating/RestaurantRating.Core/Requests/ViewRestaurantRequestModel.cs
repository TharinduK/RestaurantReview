namespace RestaurantRating.Domain
{
    public class ViewRestaurantRequestModel:TransactionRequestModel
    {
        public int RestaurantId { get; set; }
    }
}