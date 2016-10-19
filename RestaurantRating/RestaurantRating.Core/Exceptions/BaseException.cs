using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        protected BaseException()
        {

        }

        protected BaseException(string message) : base(message)
        {
        }
    }
}
