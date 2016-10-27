using System;
using System.Linq;

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
                //TODO: validate user permission 
                if (!Repository.DoseRestaurentIdExist(Request.RestaurantId))
                {
                    Response.WasSucessfull = false;
                    throw new RestaurantNotFoundException();
                }

                var fetchedReviewsForRestaurant = Repository.GetReviewsForRestaurant(Request.RestaurantId);

                Response.Reviews = fetchedReviewsForRestaurant ?? Enumerable.Empty<Review>();
                Response.WasSucessfull = true;
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