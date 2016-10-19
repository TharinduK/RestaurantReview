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
                var restaurantToUpdate = Repository.GetRestaurantById(Request.RestaurantId);
                if (restaurantToUpdate == null) throw new RestaurantNotFoundException();
                                

                if (IsRestaurantDataInRequestDifferentFromRepository(restaurantToUpdate))
                {
                    if (string.IsNullOrWhiteSpace(Request.Name)) Request.Name = restaurantToUpdate.Name;
                    else if(!restaurantToUpdate.Name.Equals(Request.Name))
                    {
                        if (Repository.DoseRestaurentNameAlreadyExist(Request.Name)) throw new RestaurantAlreadyExistsException();
                    }
                    if (string.IsNullOrWhiteSpace(Request.Cuisine)) Request.Cuisine = restaurantToUpdate.Cuisine;

                    Repository.UpdateRestaurant(Request);
                }
                Response.WasSucessfull = true;
            }
            catch (RestaurantNotFoundException)
            {
                ApplicationLog.InformationLog($"Restaurant with ID {Request.RestaurantId} not found");
                throw;
            }
            catch (RestaurantAlreadyExistsException)
            {
                ApplicationLog.InformationLog($"Restaurant name {Request.Name} exists");
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Restaurant was not updated", ex);
                Response.WasSucessfull = false;
            }
        }

        private bool IsRestaurantDataInRequestDifferentFromRepository(Restaurant restaurantToUpdate)
        {
            //comparing persistent entry to request entry 
            //because request entries can be blank if don't need to update
            if (!restaurantToUpdate.Cuisine.Equals(Request.Cuisine)) return true;
            if (!restaurantToUpdate.Name.Equals(Request.Name)) return true;

            return false;
        }
    }
}