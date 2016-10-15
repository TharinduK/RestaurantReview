namespace RestaurantRating.Domain
{
    public class AddRestaurantResponseModel : TransactionResponseModel
    {
        public int RestaurantId { get; set; }

        public override string ToString()=> $"ID:{RestaurantId} WasSucess:{WasSucessfull}";
        public override bool Equals(object obj)
        {
            var model = obj as AddRestaurantResponseModel;

            if (model == null) return false;
            else
                return RestaurantId.Equals(model.RestaurantId)
                    && WasSucessfull.Equals(model.WasSucessfull);
        }

        public override int GetHashCode() => RestaurantId.GetHashCode() + WasSucessfull.GetHashCode();
    }
}