using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public class ViewAllRestaurantsResponseModel:TransactionResponseModel
    {
        public IEnumerable<Restaurant> AllRestaurants { get; set; }
    }
}