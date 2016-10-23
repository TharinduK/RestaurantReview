namespace RestaurantRating.Domain
{
    public class UpdateRestaurantRequestModel : TransactionRequestModel
    {
        public int CuisineId { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public override string ToString() => $"Name:{Name} CuisineId Id:{CuisineId} Restaurant Id:{RestaurantId}";
    }
}