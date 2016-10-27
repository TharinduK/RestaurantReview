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
                //TODO: validate user permission 
                var restaurantFetched = Repository.GetRestaurantWithReviewsById(Request.RestaurantId);

                if (restaurantFetched == null)
                {
                    Response.WasSucessfull = false;
                    throw new RestaurantNotFoundException();
                }
                else
                {
                    Response.WasSucessfull = true;
                }
                Response.CuisineId = restaurantFetched.Cuisine.Id;
                Response.CuisineName = restaurantFetched.Cuisine.Name;
                Response.Name = restaurantFetched.Name;
                Response.RestaurantId = restaurantFetched.Id;
                Response.Reviews = restaurantFetched.Reviews;
                Response.AverageRating = restaurantFetched.AverageRating;
                Response.ReviewCount = restaurantFetched.ReviewCount;
            }
            catch (RestaurantNotFoundException)
            {
                ApplicationLog.InformationLog($"Restaurant with ID {Request.RestaurantId} not found");
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error retrieving restaurant Id {Request.RestaurantId}", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}