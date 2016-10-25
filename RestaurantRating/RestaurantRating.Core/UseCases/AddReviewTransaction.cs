using System;

namespace RestaurantRating.Domain
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
                if (!Repository.DoseRestaurentIdExist(Request.RestaurantId)) throw new RestaurantNotFoundException($"Restaurant ID :{Request.RestaurantId} not found");
                if (!Repository.DoseUserIdAlreadyExist(Request.UserId)) throw new UserNotFoundException($"User Id :{Request.UserId} not found");

                var newReviewNumber = Repository.AddReviewGetNewId(Request);
                Response.ReviewNumber = newReviewNumber;
                Response.WasSucessfull = true;
            }
            catch(RestaurantNotFoundException )
            {
                ApplicationLog.InformationLog($"Restaurant ID :{Request.RestaurantId} not found");
                throw;
            }
            catch (UserNotFoundException)
            {
                ApplicationLog.InformationLog($"User Id :{Request.UserId} not found");
                throw;
            }
            catch (Exception ex)
            {
                ApplicationLog.ErrorLog("Unable to add review", ex);
                Response.WasSucessfull = false;
            }
        }
    }
}
