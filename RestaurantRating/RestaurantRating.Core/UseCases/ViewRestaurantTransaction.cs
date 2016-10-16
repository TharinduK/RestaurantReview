using System;

namespace RestaurantRating.Domain
{
    public class ViewRestaurantTransaction : Transaction<ViewRestaurantRequestModel, ViewRestaurantResponseModel>
    {
        public ViewRestaurantTransaction(IRepository repo, IApplicationLog log, ViewRestaurantRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                var restaurantFetched = Repository.GetRestaurantWithReviewsById(Request);
                if (restaurantFetched == null)
                {
                    Response.WasSucessfull = false;
                    restaurantFetched = Restaurant.Null;
                }
                else
                {
                    Response.WasSucessfull = true;
                }
                Response.Cuisine = restaurantFetched.Cuisine;
                    Response.Name = restaurantFetched.Name;
                    Response.RestaurantId = restaurantFetched.Id;
                    Response.Reviews = restaurantFetched.Reviews;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error retrieving restaurant Id {Request.RestaurantId}", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}