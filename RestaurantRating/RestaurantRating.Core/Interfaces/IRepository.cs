﻿namespace RestaurantRating.Domain
{
    public interface IRepository
    {
        object GetAdminUser();
        int AddRestaurentGetNewID(AddRestaurantRequestModel request);
        bool DoseRestaurentAlreadyExist(AddRestaurantRequestModel request);
        void RemoveRestaurentID(RemoveRestaurantRequestModel request);
        bool DoseRestaurentIdAlreadyExist(int restaurantId);
        Restaurant GetRestaurantByID(int restaurantId);
        int AddReviewGetNewId(AddReviewRequestModel reviewToAdd);
        User GetUserById(int userId);
        bool DoseUserIdAlreadyExist(int requestUserId);
    }
}