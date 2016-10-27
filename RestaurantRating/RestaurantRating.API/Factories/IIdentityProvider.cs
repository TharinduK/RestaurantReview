namespace RestaurantRating.API
{
    public interface IIdentityProvider
    {
        int GetRequestingUserId();
    }
}