using System;

namespace RestaurantRating.Domain
{
    public class ViewCuisineTransaction : Transaction<ViewCuisinesRequestModel, ViewCuisineResponseModel>
    {
        public ViewCuisineTransaction(IRepository repo, IApplicationLog log, ViewCuisinesRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                Response.Cuisines = Repository.GetAllCuisines();
                Response.WasSucessfull = true;

            }
            catch (Exception ex)
            {
                Response.WasSucessfull = false;
                ApplicationLog.ErrorLog("Error retrieving cuisines", ex);
                throw;
            }
            
        }
    }
}