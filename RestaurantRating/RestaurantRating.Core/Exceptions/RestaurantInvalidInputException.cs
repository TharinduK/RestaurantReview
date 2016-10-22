namespace RestaurantRating.Domain
{
    public class RestaurantInvalidInputException : RestaurantConstrantException
    {
        public RestaurantInvalidInputException(string message) : base(message)
        {
        }
    }
}