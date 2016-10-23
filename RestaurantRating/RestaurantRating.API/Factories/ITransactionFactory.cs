using RestaurantRating.Domain;

namespace RestaurantRating.API.Factories
{
    public interface ITransactionFactory
    {
        AddRestaurantTransaction CreateAddRestraurantTransaction(string name, int cuisineId);

        CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(int restaurantId, string name,
            int cuisineId);
        RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove);

        PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(int restaurantId, string name,
            int cuisineId);
        ViewAllRestaurantsTransaction CreateViewAllRestaurantsTransaction();
        ViewRestaurantTransaction CreateViewRestaurantTransaction(int restaurantId);
        ViewReviewsForRestaurantTransaction CreateViewReviewsForRestaurantTransaction(int restaurantId);
    }
}