using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public class ViewRestaurantsResponseModel:TransactionResponseModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}