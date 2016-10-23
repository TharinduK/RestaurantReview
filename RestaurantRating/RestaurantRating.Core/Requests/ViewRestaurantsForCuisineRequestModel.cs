namespace RestaurantRating.Domain
{
    public class ViewRestaurantsForCuisineRequestModel : TransactionRequestModel
    {
        public int CuisineId { get; set; }
    }
}