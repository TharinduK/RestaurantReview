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
            foreach (var rest in tran.Response.AllRestaurants)
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
                Cuisine = rest.Cuisine,
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
                Cuisine = tran.Response.Cuisine,
                ReviewCount = tran.Response.ReviewCount,
                AverageRating = tran.Response.AverageRating
            };
            return rest;
        }
    }
}