using System;

namespace RestaurantRating.Domain
{
    [Serializable]
    public class UserNotFoundException : RestaurantConstrantException
    {
        public UserNotFoundException()
        {

        }
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}