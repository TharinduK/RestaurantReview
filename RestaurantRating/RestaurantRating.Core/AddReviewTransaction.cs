using System;
using System.Runtime.CompilerServices;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    public class AddReviewTransaction : Transaction<AddReviewRequestModel, AddReviewResponseModel>
    {
        public AddReviewTransaction(IRepository repo, IApplicationLog log, AddReviewRequestModel reqeustModel)
            : base(repo, log, reqeustModel)
        {
        }

        public override void Execute()
        {
            try
            {

                Response.WasSucessfull = true;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Unable to add review", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}
