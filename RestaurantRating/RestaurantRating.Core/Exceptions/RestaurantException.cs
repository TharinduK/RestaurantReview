using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {

        }
        public BaseException(string message) : base(message)
        {
        }
    }

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
