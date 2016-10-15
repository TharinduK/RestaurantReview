using System;
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

                if (!Repository.DoseRestaurentIdAlreadyExist(Request.RestaruntId))
                {
                    throw new RestaurantException("Restaurant not found");
                }
                if(!Repository.DoseUserIdAlreadyExist(Request.UserId))
                {
                    throw new RestaurantException("User not found");
                }

                var newReviewNumber = Repository.AddReviewGetNewId(Request);
                Response.ReviewNumber = newReviewNumber;
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
