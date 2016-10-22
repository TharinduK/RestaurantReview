using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantNotFoundException : RestaurantConstrantException
    {
        public RestaurantNotFoundException()
        {

        }
        public RestaurantNotFoundException(string message) : base(message)
        {
        }
    }
}