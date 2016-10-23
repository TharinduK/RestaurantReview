using System;

namespace RestaurantRating.Domain
{
    public abstract class UpdateRestaurantTransaction : Transaction<UpdateRestaurantRequestModel, UpdateRestaurantResponseModel>
    {
        protected UpdateRestaurantTransaction(IRepository repo, IApplicationLog log, UpdateRestaurantRequestModel reqeustModel) : base(repo, log, reqeustModel)
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
                    UpdateRequesWithMissingData(restaurantToUpdate);

                    ValidateInputRequest();

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
            catch (RestaurantInvalidInputException ex)
            {
                ApplicationLog.InformationLog(ex.Message);
                throw;
            }
            catch (CuisineNotFoundException ex)
            {
                ApplicationLog.InformationLog(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Restaurant was not updated", ex);
                Response.WasSucessfull = false;
            }
        }

        private void ValidateInputRequest()
        {
            if (string.IsNullOrWhiteSpace(Request.Name))
            {
                throw new RestaurantInvalidInputException("Restaurant name is blank");
            }

            if(Request.CuisineId == 0)
            {
                throw new RestaurantInvalidInputException("Restaurant cuisine is not specified");
            }

            if (!Repository.DoseCuisineIdExist(Request.CuisineId)) throw new CuisineNotFoundException();
        }

        protected abstract void UpdateRequesWithMissingData(Restaurant restaurantToUpdate);

        private bool IsRestaurantDataInRequestDifferentFromRepository(Restaurant restaurantToUpdate)
        {
            //comparing persistent entry to request entry 
            //because request entries can be blank if don't need to update
            if (!restaurantToUpdate.Cuisine.Id.Equals(Request.CuisineId)) return true;
            if (!restaurantToUpdate.Name.Equals(Request.Name)) return true;

            return false;
        }
    }
}