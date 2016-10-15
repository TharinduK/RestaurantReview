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
                if (!Repository.DoseRestaurentIdAlreadyExist(Request.RestaurantId))
                {
                    Response.WasSucessfull = false;
                    return;
                }
                else
                {
                    Repository.RemoveRestaurentID(Request);
                    Response.WasSucessfull = true;
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error removing restaurant ID {Request.RestaurantId}", ex);
                Response.WasSucessfull = false;
            }
            
            
        }
    }
}
