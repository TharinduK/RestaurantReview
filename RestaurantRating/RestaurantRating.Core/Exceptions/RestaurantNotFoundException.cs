using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantNotFoundException : BaseException
    {
        public RestaurantNotFoundException()
        {

        }
        public RestaurantNotFoundException(string message) : base(message)
        {
        }
    }
}