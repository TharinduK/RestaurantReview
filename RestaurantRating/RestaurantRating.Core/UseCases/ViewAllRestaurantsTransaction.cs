﻿using System;

namespace RestaurantRating.Domain
{
    public class ViewAllRestaurantsTransaction :
        Transaction<ViewAllRestaurantRequestModel, ViewAllRestaurantsResponseModel>
    {
        public ViewAllRestaurantsTransaction(IRepository repo, IApplicationLog log,
            ViewAllRestaurantRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {
                //validate permission to run app

                var allRestaurants = Repository.GetAllRestaurantsWithReview();
                Response.AllRestaurants = allRestaurants;
                
                Response.WasSucessfull = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Error fetching all restaurants", ex);
                Response.WasSucessfull = false;
            }

        }
    }
}