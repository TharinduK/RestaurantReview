using System.Collections.Generic;
using System.Linq;


namespace RestaurantRating.Domain
{
    public class ViewRestaurantResponseModel : TransactionResponseModel
    {
        public string Name;
        public int CuisineId;
        public string CuisineName;
        public int RestaurantId;
        public IEnumerable<Review> Reviews { get; set; } = Enumerable.Empty<Review>();
        public double AverageRating;
        public int ReviewCount;

        public override string ToString()
            => $"Name:{Name} CuisineId Id:{CuisineId} Restaurant Id:{RestaurantId} ";

        public override bool Equals(object obj)
        {
            var model = obj as ViewRestaurantResponseModel;

            if (model == null) return false;
            else
                return RestaurantId.Equals(model.RestaurantId)
                    && WasSucessfull.Equals(model.WasSucessfull)
                    && Name.Equals(model.Name)
                    && CuisineId.Equals(model.CuisineId)
                    && Reviews.SequenceEqual(model.Reviews);
        }
        public override int GetHashCode()
            => Name.GetHashCode() + CuisineId.GetHashCode() + RestaurantId.GetHashCode() + Reviews.GetHashCode();

    }
}