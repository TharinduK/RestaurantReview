using RestaurantRating.Domain.UseCases;
using System;
using System.Linq;

namespace RestaurantRating.Domain
{
    public class ViewRestaurantsForCuisineTransaction:Transaction<ViewRestaurantForCuisineRequestModel, ViewRestaurantsResponseModel>
    {
        public ViewRestaurantsForCuisineTransaction(IRepository repo, IApplicationLog log, ViewRestaurantForCuisineRequestModel reqeustModel) 
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                if (!Repository.DoseCuisineIdAlreadyExist(Request.CusineID))
                {
                    Response.WasSucessfull = false;
                    throw new CuisineNotFoundException();
                }
                var fetchedRestaurants = Repository.GetRestaurantForCuisine(Request.CusineID);

                Response.Restaurants = fetchedRestaurants?? Enumerable.Empty<Restaurant>();
                Response.WasSucessfull = true;
            }
            catch (CuisineNotFoundException)
            {
                ApplicationLog.InformationLog($"Cuisine ID {Request.CusineID} not found");
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog($"Error retrieving restaurants with Cuisine Id {Request.CusineID}", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}