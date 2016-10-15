namespace RestaurantRating.Domain
{
    public class AddRestaurantRequestModel : TransactionRequestModel
    {
        public string Cuisine { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"Cuisine:{Cuisine} Name:{Name} UserID{UserId}";

        public override bool Equals(object obj)
        {
            AddRestaurantRequestModel model = obj as AddRestaurantRequestModel;

            if (model == null) return false;
            else
                return UserId.Equals(model.UserId)
                    && Name.Equals(model.Name, System.StringComparison.CurrentCultureIgnoreCase)
                    && Cuisine.Equals(model.Cuisine);
        }

        public override int GetHashCode() => UserId.GetHashCode() + Name.GetHashCode() + Cuisine.GetHashCode();
    }
}