using System;

namespace RestaurantRating.Domain
{
    public class AddRestaurentTransaction : Transaction <AddRestaurantRequestModel, AddRestaurantResponseModel>
    {
        public AddRestaurentTransaction(IRepository repo, IApplicationLog log, AddRestaurantRequestModel req)
            :base(repo, log, req)
        {
        }

        public override void Execute()
        {
            try
            {
                if (Repository.DoseRestaurentAlreadyExist(Request)) throw new RestaurantException("Restaurant with same attributes already exists");

                Response.RestaurantId = Repository.AddRestaurentGetNewId(Request);
            //var user = _repository.GetAdminUser();

                //var admiUser = user as Administrator;
                //if(admiUser == null) throw new InvalidOperationExceptio

                Response.WasSucessfull = true;
            }
            catch(Exception ex)
            {
                ApplicationLog.ErrorLog("Error adding new restaurant", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}
