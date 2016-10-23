using System.Collections.Generic;
using RestaurantRating.Domain;
using Restaurant = RestaurantRating.API.ViewModels.Restaurant;

namespace RestaurantRating.API
{
    public class ViewModelMapper
    {
        public IEnumerable<Restaurant> ConvertDomainRestaurantToViewModel(ViewAllRestaurantsTransaction tran)
        {
            var allRestaurants = new List<Restaurant>();
            foreach (var rest in tran.Response.Restaurants)
            {
                allRestaurants.Add(ConvertDomainRestaurantToViewModel(rest));
            }
            return allRestaurants;
        }

        public Restaurant ConvertDomainRestaurantToViewModel(Domain.Restaurant rest)
        {
            var restViewModel = new ViewModels.Restaurant
            {
                Id = rest.Id,
                Name = rest.Name,
                CuisineId = rest.Cuisine.Id,
                CuisineName = rest.Cuisine.Name,
                ReviewCount = rest.ReviewCount,
                AverageRating = rest.AverageRating
            };
            return restViewModel;
        }

        public static Restaurant ConvertDomainRestaurantToViewModel(int id, ViewRestaurantTransaction tran)
        {
            ViewModels.Restaurant rest = new ViewModels.Restaurant
            {
                Id = id,
                Name = tran.Response.Name,
                CuisineId = tran.Response.CuisineId,
                CuisineName = tran.Response.CuisineName,
                ReviewCount = tran.Response.ReviewCount,
                AverageRating = tran.Response.AverageRating
            };
            return rest;
        }
    }
}