using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantAlreadyExistsException : BaseException
    {
        public RestaurantAlreadyExistsException()
        {
        }
        public RestaurantAlreadyExistsException(string message) : base(message)
        {
        }
    }
}