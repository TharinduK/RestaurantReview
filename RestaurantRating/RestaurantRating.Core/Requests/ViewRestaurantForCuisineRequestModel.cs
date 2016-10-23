namespace RestaurantRating.Domain
{
    public class ViewRestaurantForCuisineRequestModel : TransactionRequestModel
    {
        public int CusineID { get; set; }
    }
}