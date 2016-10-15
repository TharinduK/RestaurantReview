using System;

namespace RestaurantRating.Domain
{
    public class UpdateRestaurantTransaction : Transaction<UpdateRestaurantRequestModel, UpdateRestaurantResponseModel>
    {
        public UpdateRestaurantTransaction(IRepository repo, IApplicationLog log, UpdateRestaurantRequestModel reqeustModel) : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                var restaurantToUpdate = Repository.GetRestaurantByID(Request.RestaurantId);
                if (restaurantToUpdate == null) throw new RestaurantException($"Restaurant RestaurantId {Request.RestaurantId} not found in repository");

                if (WasRestaurantDataUpdatedInRequest(restaurantToUpdate))
                {
                    if(!string.IsNullOrWhiteSpace(Request.Name)) restaurantToUpdate.Name = Request.Name;
                    if (!string.IsNullOrWhiteSpace(Request.Cuisine)) restaurantToUpdate.Cuisine = Request.Cuisine;
                    restaurantToUpdate.UpdatedBy = Request.UserId;
                }
                Response.WasSucessfull = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Restaurant was not updated", ex);
                Response.WasSucessfull = false;
            }
        }

        private bool WasRestaurantDataUpdatedInRequest(Restaurant restaurantToUpdate)
        {
            //comparing persistent entry to request entry 
            //because request entries can be blank if don't need to update
            if (!restaurantToUpdate.Cuisine.Equals(Request.Cuisine)) return true;
            if (!restaurantToUpdate.Name.Equals(Request.Name)) return true;

            return false;
        }
    }
}