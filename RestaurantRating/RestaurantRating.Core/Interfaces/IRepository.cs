namespace RestaurantRating.Domain
{
    public interface IRepository
    {
        object GetAdminUser();
        int AddRestaurentGetNewId(AddRestaurantRequestModel request);
        bool DoseRestaurentAlreadyExist(AddRestaurantRequestModel request);
        void RemoveRestaurentId(RemoveRestaurantRequestModel request);
        bool DoseRestaurentIdAlreadyExist(int restaurantId);
        Restaurant GetRestaurantById(int restaurantId);
        int AddReviewGetNewId(AddReviewRequestModel reviewToAdd);
        User GetUserById(int userId);
        bool DoseUserIdAlreadyExist(int requestUserId);
        Restaurant GetRestaurantWithReviewsById(ViewRestaurantRequestModel request);
    }
}