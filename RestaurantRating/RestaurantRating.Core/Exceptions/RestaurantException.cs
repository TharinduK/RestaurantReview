using System;

namespace RestaurantRating.Domain
{
    public class RestaurantException : Exception
    {
        public RestaurantException(string message) : base(message)
        {
        }
    }
}
