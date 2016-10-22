namespace RestaurantRating.Domain
{
    public class RestaurantConstrantException : BaseException
    {
        public RestaurantConstrantException()
        {

        }

        public RestaurantConstrantException(string message) : base(message)
        {
        }
    }
}