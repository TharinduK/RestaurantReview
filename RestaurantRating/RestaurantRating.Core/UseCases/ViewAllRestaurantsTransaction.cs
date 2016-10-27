using System;

namespace RestaurantRating.Domain
{
    public class ViewAllRestaurantsTransaction :
        Transaction<ViewAllRestaurantRequestModel, ViewRestaurantsResponseModel>
    {
        public ViewAllRestaurantsTransaction(IRepository repo, IApplicationLog log,
            ViewAllRestaurantRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                //TODO: validate user permission 

                var allRestaurants = Repository.GetAllRestaurantsWithReview();
                Response.Restaurants = allRestaurants;
                
                Response.WasSucessfull = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Error fetching all restaurants", ex);
                Response.WasSucessfull = false;
            }

        }
    }
}