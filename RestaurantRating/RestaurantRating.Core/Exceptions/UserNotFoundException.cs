using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException()
        {

        }
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}