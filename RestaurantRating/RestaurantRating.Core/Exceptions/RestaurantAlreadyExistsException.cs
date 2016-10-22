using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantAlreadyExistsException : RestaurantConstrantException
    {
        public RestaurantAlreadyExistsException()
        {
        }
        public RestaurantAlreadyExistsException(string message) : base(message)
        {
        }
    }
}