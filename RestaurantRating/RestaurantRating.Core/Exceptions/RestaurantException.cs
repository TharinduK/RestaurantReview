using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantException : Exception
    {
        public RestaurantException(string message) : base(message)
        {
        }
    }
}
