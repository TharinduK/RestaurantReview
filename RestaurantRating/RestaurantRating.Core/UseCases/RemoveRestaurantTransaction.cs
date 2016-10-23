using System;

namespace RestaurantRating.Domain
{
    public class RemoveRestaurantTransaction : Transaction<RemoveRestaurantRequestModel, RemoveRestaurantResponseModel>
    {
        public RemoveRestaurantTransaction(IRepository repo, IApplicationLog log, RemoveRestaurantRequestModel requestModel) : base(repo, log, requestModel)
        {
        }

        public override void Execute()
        {
            try
            {
                if (!Repository.DoseRestaurentIdExist(Request.RestaurantId))
                {
                    Response.WasSucessfull = false;
                    throw new RestaurantNotFoundException();
                }
                else
                {
                    Repository.RemoveRestaurentId(Request);
                    Response.WasSucessfull = true;
                }
            }
            catch (RestaurantNotFoundException) { throw; }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error removing restaurant ID {Request.RestaurantId}", ex);
                Response.WasSucessfull = false;
            }
            
            
        }
    }
}
