using System;

namespace RestaurantRating.Domain
{
    public class ViewCuisinesTransaction : Transaction<ViewCuisinesRequestModel, ViewCuisineResponseModel>
    {
        public ViewCuisinesTransaction(IRepository repo, IApplicationLog log, ViewCuisinesRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                //TODO: validate user permission 
                Response.Cuisines = Repository.GetAllCuisines();
                Response.WasSucessfull = true;

            }
            catch (Exception ex)
            {
                Response.WasSucessfull = false;
                ApplicationLog.ErrorLog("Error retrieving cuisines", ex);
            }
            
        }
    }
}