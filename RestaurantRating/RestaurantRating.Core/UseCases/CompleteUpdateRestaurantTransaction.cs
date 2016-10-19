namespace RestaurantRating.Domain
{
    public class CompleteUpdateRestaurantTransaction : UpdateRestaurantTransaction
    {
        public CompleteUpdateRestaurantTransaction(IRepository repo, IApplicationLog log, UpdateRestaurantRequestModel reqeustModel) 
            : base(repo, log, reqeustModel)
        {
        }

        protected override void UpdateRequesWithMissingData(Restaurant restaurantToUpdate)
        {
        }
    }
}