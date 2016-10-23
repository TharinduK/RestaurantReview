using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public class ViewCuisineResponseModel:TransactionResponseModel
    {
        public IEnumerable<Cuisine> Cuisines { get; set; }
    }
}