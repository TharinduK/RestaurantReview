using System;

namespace RestaurantRating.Domain
{
    public class Review :AuditRecord
    {
        public int Rating { get; set; }
        public DateTime PostedDateTime { get; set; }
        public string Comment { get; set; }
        public int ReviewNumber { get; set; }
        public Restaurant ReviewRestaurant { get; set; }
        public User ReviewUser { get; set; }
    }
}