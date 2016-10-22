using System.Collections.Generic;
using System.Linq;


namespace RestaurantRating.Domain
{
    public class ViewRestaurantResponseModel : TransactionResponseModel
    {
        public string Name;
        public string Cuisine;
        public int RestaurantId;
        public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
        public double AverageRating;
        public int ReviewCount;

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