using System;

namespace RestaurantRating.Domain
{
    public class AddRestaurantTransaction : Transaction <AddRestaurantRequestModel, AddRestaurantResponseModel>
    {
        public AddRestaurantTransaction(IRepository repo, IApplicationLog log, AddRestaurantRequestModel req)
            :base(repo, log, req)
        {
        }

        public override void Execute()
        {
            try
            {
                //TODO: validate user permission 
                if (Repository.DoseRestaurentNameAlreadyExist(Request.Name))
                    throw new RestaurantAlreadyExistsException();

                Response.RestaurantId = Repository.AddRestaurentGetNewId(Request);

                Response.WasSucessfull = true;
            }
            catch (RestaurantAlreadyExistsException)
            {
                ApplicationLog.InformationLog($"Restaurant name {Request.Name} already exists");
                throw;
            }
            catch(Exception ex)
            {
                ApplicationLog.ErrorLog("Error adding new restaurant", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}
