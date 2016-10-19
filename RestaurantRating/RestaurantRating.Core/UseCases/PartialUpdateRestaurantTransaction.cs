namespace RestaurantRating.Domain
{
    public class PartialUpdateRestaurantTransaction:UpdateRestaurantTransaction
    {
        public PartialUpdateRestaurantTransaction(IRepository repo, IApplicationLog log, UpdateRestaurantRequestModel reqeustModel) : 
            base(repo, log, reqeustModel)
        {
        }

        protected override void UpdateRequesWithMissingData(Restaurant restaurantToUpdate)
        {
            if (string.IsNullOrWhiteSpace(Request.Name)) Request.Name = restaurantToUpdate.Name;
            else if (!restaurantToUpdate.Name.Equals(Request.Name))
            {
                if (Repository.DoseRestaurentNameAlreadyExist(Request.Name)) throw new RestaurantAlreadyExistsException();
            }
            if (string.IsNullOrWhiteSpace(Request.Cuisine)) Request.Cuisine = restaurantToUpdate.Cuisine;
        }
    }
}