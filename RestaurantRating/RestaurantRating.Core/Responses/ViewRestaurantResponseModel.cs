using System.Collections.Generic;
using System.Linq;

namespace RestaurantRating.Domain
{
    public class ViewRestaurantResponseModel : TransactionResponseModel
    {
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public int RestaurantId { get; set; }
        public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public override string ToString()
            => $"Name:{Name} Cuisine:{Cuisine} Restaurant Id:{RestaurantId} ";

        public override bool Equals(object obj)
        {
            var model = obj as ViewRestaurantResponseModel;

            if (model == null) return false;
            else
                return RestaurantId.Equals(model.RestaurantId)
                    && WasSucessfull.Equals(model.WasSucessfull)
                    && Name.Equals(model.Name)
                    && Cuisine.Equals(model.Cuisine)
                    && Reviews.SequenceEqual(model.Reviews);
        }
        public override int GetHashCode()
            => Name.GetHashCode() + Cuisine.GetHashCode() + RestaurantId.GetHashCode() + Reviews.GetHashCode();

    }
}