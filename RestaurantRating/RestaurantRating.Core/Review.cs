using System;

namespace RestaurantRating.Domain
{
    public class Review :AuditRecord
    {
        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (value > 5 || value < 1)
                {
                    throw new IndexOutOfRangeException(
                        $"Specified rating {value} is outside the valid range between 1 and 5");
                }
                _rating = value;
            }
        }

        public DateTime PostedDateTime { get; set; }
        public string Comment { get; set; }
        public int ReviewNumber { get; set; }
        public Restaurant ReviewRestaurant { get; set; }
        public User ReviewUser { get; set; }
    }
}