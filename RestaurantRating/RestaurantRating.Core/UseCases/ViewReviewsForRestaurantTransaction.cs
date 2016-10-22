using System;

namespace RestaurantRating.Domain
{
    public class ViewReviewsForRestaurantTransaction:Transaction<ViewRestaurantRequestModel, ViewReviewsForRestaurantResponseModel>
    {
        public ViewReviewsForRestaurantTransaction(IRepository repo, IApplicationLog log, ViewRestaurantRequestModel reqeustModel) 
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                var fetchedReviewsForRestaurant = Repository.GetReviewsForRestaurant(Request.RestaurantId);

                if (fetchedReviewsForRestaurant == null)
                {
                    Response.WasSucessfull = false;
                    throw new RestaurantNotFoundException();
                }
                else
                {
                    Response.WasSucessfull = true;
                }
                Response.Reviews = fetchedReviewsForRestaurant;
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