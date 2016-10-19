namespace RestaurantRating.Domain
{
    public class RestaurantInvalidInputException : BaseException
    {
        public RestaurantInvalidInputException(string message) : base(message)
        {
        }
    }
}