using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class RestaurantInvalidInputException : BaseException
    {
        public RestaurantInvalidInputException(string message) : base(message)
        {
        }
    }
}