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
            Restaurant restaurantFetched = null;  //TODO: conver to a DTO
            try
            {
                restaurantFetched = Repository.GetRestaurantWithReviewsById(Request);
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error retrieving restaurant Id {Request.RestaurantId}", ex);
                Response.WasSucessfull = false;
            }

            if (restaurantFetched == null)
            {
                restaurantFetched = Restaurant.Null;
                Response.WasSucessfull = false;
                throw new RestaurantNotFoundException($"Restaurant with ID {Request.RestaurantId} not found");
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
    }
}