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
        //public Restaurant ReviewRestaurant { get; set; }
        public User ReviewUser { get; set; }

        public override bool Equals(object obj)
        {
            var model = obj as Review;

            if (model == null) return false;
            else
                return Rating.Equals(model.Rating)
                       && PostedDateTime.Equals(model.PostedDateTime)
                       && Comment.Equals(model.Comment)
                       && ReviewNumber.Equals(model.ReviewNumber)
                       && ReviewUser.Equals(model.ReviewUser);
            //&& ReviewRestaurant.Equals(model.ReviewRestaurant);
        }
        public override int GetHashCode()
        {
            return Rating.GetHashCode() + PostedDateTime.GetHashCode() + Comment.GetHashCode() +
                   ReviewNumber.GetHashCode() + ReviewUser.GetHashCode();
        }

        public override string ToString()
            =>
            $"Review #:{ReviewNumber} Rating:{Rating} Posted Date{PostedDateTime} Comment:{Comment} UserID:{ReviewUser.Id}";
    }
}