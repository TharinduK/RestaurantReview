using System;
using System.Linq;

namespace RestaurantRating.Domain
{
    public class ViewRestaurantsForCuisineTransaction:Transaction<ViewRestaurantsForCuisineRequestModel, ViewRestaurantsResponseModel>
    {
        public ViewRestaurantsForCuisineTransaction(IRepository repo, IApplicationLog log, ViewRestaurantsForCuisineRequestModel reqeustModel) 
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                //TODO: validate user permission 
                if (!Repository.DoseCuisineIdExist(Request.CuisineId))
                {
                    Response.WasSucessfull = false;
                    throw new CuisineNotFoundException();
                }
                var fetchedRestaurants = Repository.GetRestaurantForCuisine(Request.CuisineId);

                Response.Restaurants = fetchedRestaurants?? Enumerable.Empty<Restaurant>();
                Response.WasSucessfull = true;
            }
            catch (CuisineNotFoundException)
            {
                ApplicationLog.InformationLog($"CuisineId ID {Request.CuisineId} not found");
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error retrieving restaurants with CuisineId Id {Request.CuisineId}", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}