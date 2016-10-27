namespace RestaurantRating.API
{
    public class IdentityProvider : IIdentityProvider
    {
        public int GetRequestingUserId()
        {
            //TODO: update logic to capture identity from identity service 
            return 1;
        }
    }
}