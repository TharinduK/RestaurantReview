using System.Collections.Generic;

namespace RestaurantRating.Domain
{
    public interface IRepository
    {
        object GetAdminUser();
        int AddRestaurentGetNewId(AddRestaurantRequestModel requestModel);
        bool DoseRestaurentNameAlreadyExist(string restaurantNameToCheck);
        void RemoveRestaurentId(RemoveRestaurantRequestModel reqeustModel);
        bool DoseRestaurentIdAlreadyExist(int restaurantId);
        Restaurant GetRestaurantById(int restaurantId);
        int AddReviewGetNewId(AddReviewRequestModel reviewToAdd);
        IEnumerable<Review> GetReviewsForRestaurant(int restaurantId);
        User GetUserById(int userId);
        bool DoseUserIdAlreadyExist(int requestUserId);
        Restaurant GetRestaurantWithReviewsById(int restaurantId);
        void UpdateRestaurant(UpdateRestaurantRequestModel request);
        IEnumerable<Restaurant> GetAllRestaurantsWithReview();
    }
}