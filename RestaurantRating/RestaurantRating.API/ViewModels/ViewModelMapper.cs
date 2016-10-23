using System.Collections.Generic;
using RestaurantRating.Domain;
using Restaurant = RestaurantRating.API.ViewModels.Restaurant;

namespace RestaurantRating.API
{
    public class ViewModelMapper
    {
        public static IEnumerable<ViewModels.Restaurant> ConvertDomainRestaurantGroupToViewModel(IEnumerable<Domain.Restaurant> domainRestaurants)
        {
            var allRestaurants = new List<ViewModels.Restaurant>();
            foreach (var rest in domainRestaurants)
            {
                allRestaurants.Add(ConvertDomainRestaurantToViewModel(rest));
            }
            return allRestaurants;
        }

        public static Restaurant ConvertDomainRestaurantToViewModel(Domain.Restaurant rest)
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

        public static IEnumerable<ViewModels.Cuisine> ConvertDomainCuisineToViewModel(ViewCuisinesTransaction tran)
        {
            var retrivedCuisines = new List<ViewModels.Cuisine>();
            foreach (var cuisine in tran.Response.Cuisines)
            {
                retrivedCuisines.Add(new ViewModels.Cuisine {Id = cuisine.Id, Name = cuisine.Name});
            }
            return retrivedCuisines;
        }

        public static IEnumerable<ViewModels.Review> ConvertDomainReviewToViewModel(IEnumerable<Domain.Review> domainReviews)
        {
            var reviews = new List<ViewModels.Review>();

            foreach (var review in domainReviews)
            {
                reviews.Add(new ViewModels.Review
                {
                    Comment = review.Comment,
                    PostedDateTime = review.PostedDateTime,
                    Rating = review.Rating,
                    ReviewNumber = review.ReviewNumber,
                    UserName = review.ReviewUser.UserName
                });
            }

            return reviews;
        }
    }
}