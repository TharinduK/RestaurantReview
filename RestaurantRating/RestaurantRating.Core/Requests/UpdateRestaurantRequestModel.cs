namespace RestaurantRating.Domain
{
    public class UpdateRestaurantRequestModel : TransactionRequestModel
    {
        public string Cuisine { get; set; }
        public string Name { get; set; }
        public int RestaurantId { get; set; }

        public override string ToString() => $"Name:{Name} Cuisine:{Cuisine} Restaurant Id:{RestaurantId}";
    }
}