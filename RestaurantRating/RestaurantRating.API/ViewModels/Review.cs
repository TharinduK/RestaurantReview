using System;

namespace RestaurantRating.API.ViewModels
{
    public class Review
    {
        public int Rating;
        public DateTime PostedDateTime;
        public string Comment;
        public int ReviewNumber;
        public string UserName;
    }
}