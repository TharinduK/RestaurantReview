using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public interface IRepository
    {
        bool DoseUserIdAlreadyExist(int requestUserId);
        User GetUserById(int userId);

        bool DoseRestaurentNameAlreadyExist(string restaurantNameToCheck);
        bool DoseRestaurentIdExist(int restaurantId);
        IEnumerable<Restaurant> GetRestaurantForCuisine(int requestCusineId);
        Restaurant GetRestaurantById(int restaurantId);
        Restaurant GetRestaurantWithReviewsById(int restaurantId);
        IEnumerable<Restaurant> GetAllRestaurantsWithReview();
        int AddRestaurentGetNewId(AddRestaurantRequestModel requestModel);
        void UpdateRestaurant(UpdateRestaurantRequestModel request);
        void RemoveRestaurentId(RemoveRestaurantRequestModel reqeustModel);


        IEnumerable<Review> GetReviewsForRestaurant(int restaurantId);
        int AddReviewGetNewId(AddReviewRequestModel reviewToAdd);

        bool DoseCuisineIdExist(int requestCusineId);
        IEnumerable<Cuisine> GetAllCuisines();
        Cuisine GetCuisineById(int cuisineId);

    }
}