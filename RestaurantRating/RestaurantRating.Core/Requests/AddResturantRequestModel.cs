namespace RestaurantRating.Domain
{
    public class AddRestaurantRequestModel : TransactionRequestModel
    {
        public int CuisineId { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"CuisineId:{CuisineId} Name:{Name} UserID{UserId}";

        public override bool Equals(object obj)
        {
            AddRestaurantRequestModel model = obj as AddRestaurantRequestModel;

            if (model == null) return false;
            else
                return UserId.Equals(model.UserId)
                    && Name.Equals(model.Name, System.StringComparison.CurrentCultureIgnoreCase)
                    && CuisineId.Equals(model.CuisineId);
        }

        public override int GetHashCode() => UserId.GetHashCode() + Name.GetHashCode() + CuisineId.GetHashCode();
    }
}