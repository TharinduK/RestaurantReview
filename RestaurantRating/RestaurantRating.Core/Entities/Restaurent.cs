using System.Collections.Generic;
using System.Linq;

namespace RestaurantRating.Domain
{
    public class Restaurant : AuditRecord
    {
        private string _cuisine;//TODO: must update cuisine to use a cuisine object (not string)
        private string _name;
        private List<Review> _reviews;

        private class NullRestaurant : Restaurant
        {
            public override string Cuisine => string.Empty;
            public override string Name => string.Empty;
            public override void AddReview(Review reviewToAdd){}
            public override double AverageRating => 0;
            public override int ReviewCount => 0;
        }

        public static readonly Restaurant Null = new NullRestaurant();

        public virtual string Cuisine
        {
            get { return _cuisine; }
            set { _cuisine = value.Trim(); }
        }
        public int Id { get; set; }
        public virtual string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }
        public IEnumerable<Review> Reviews
        {
            get
            {
                if(_reviews == null) return Enumerable.Empty<Review>();
                return _reviews;
            }
        }


        public virtual double AverageRating => _reviews?.Average(r => r.Rating) ?? 0;
        public virtual int ReviewCount => _reviews?.Count ?? 0;
        public virtual void AddReview(Review reviewToAdd)
        {
            if (_reviews == null) _reviews = new List<Review>();
            _reviews.Add(reviewToAdd);
        }


        public override bool Equals(object obj)
        {
            var model = obj as Restaurant;

            if (model == null) return false;
            else
                return Cuisine.Equals(model.Cuisine)
                    && Name.Equals(model.Name)
                    && Id.Equals(model.Id)
                    && Reviews.SequenceEqual(model.Reviews);
        }
        public override int GetHashCode()
        {
            return Cuisine.GetHashCode() + Name.GetHashCode() + Id.GetHashCode() + Reviews.GetHashCode();
        }
        public override string ToString() => $"Cuisine:{Cuisine} Name:{Name} Id:{Id} Review Count:{Reviews.Count()}";

    }
}
