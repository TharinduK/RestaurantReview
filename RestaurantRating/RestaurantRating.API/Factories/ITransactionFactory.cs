using RestaurantRating.Domain;

namespace RestaurantRating.API.Factories
{
    public interface ITransactionFactory
    {
        AddRestaurantTransaction CreateAddRestraurantTransaction(string name, string cuisine);

        CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(int restaurantId, string name,
            string cuisine);
        RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove);

        PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(int restaurantId, string name,
            string cuisine);
        ViewAllRestaurantsTransaction CreateViewAllRestaurantsTransaction();
        ViewRestaurantTransaction CreateViewRestaurantTransaction(int restaurantId);
        ViewReviewsForRestaurantTransaction CreateViewReviewsForRestaurantTransaction(int restaurantId);
    }
}