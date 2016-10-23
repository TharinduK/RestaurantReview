using System;
using RestaurantRating.Domain;
using Review = RestaurantRating.API.ViewModels.Review;

namespace RestaurantRating.API.Factories
{
    public interface ITransactionFactory
    {
        AddRestaurantTransaction CreateAddRestraurantTransaction(string name, int cuisineId);

        CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(int restaurantId, string name,
            int cuisineId);
        PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(int restaurantId, string name,
            int cuisineId);
        RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove);


        ViewAllRestaurantsTransaction CreateViewAllRestaurantsTransaction();
        ViewRestaurantsForCuisineTransaction CreateViewRestaurantsForCuisineTransaction(int id);
        ViewRestaurantTransaction CreateViewRestaurantTransaction(int restaurantId);

        ViewReviewsForRestaurantTransaction CreateViewReviewsForRestaurantTransaction(int restaurantId);

        ViewCuisinesTransaction CreateViewAllCuisinesTransaction();

        AddReviewTransaction CreateAddReviewsForRestaurantTransaction(int restaurantId, Review reviewRequest);
    }
}