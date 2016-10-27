using RestaurantRating.Domain;
using Review = RestaurantRating.API.ViewModels.Review;

namespace RestaurantRating.API
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly IRepository _repo;
        private readonly IApplicationLog _log;
        private readonly IIdentityProvider _identity;

        public TransactionFactory(IRepository repo, IApplicationLog log, IIdentityProvider identity)
        {
            _repo = repo;
            _log = log;
            _identity = identity;

        }

        public ViewRestaurantsForCuisineTransaction CreateViewRestaurantsForCuisineTransaction(int id)
        {
            var reqModel = new ViewRestaurantsForCuisineRequestModel
            {
                CuisineId = id,
                UserId = _identity.GetRequestingUserId()
            };
            return new ViewRestaurantsForCuisineTransaction(_repo, _log, reqModel);
        }

        public ViewRestaurantTransaction CreateViewRestaurantTransaction(int restaurantId)
        {
            var reqModel = new ViewRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                UserId = _identity.GetRequestingUserId()
            };
            return new ViewRestaurantTransaction(_repo, _log, reqModel);
        }

        public AddRestaurantTransaction CreateAddRestraurantTransaction(string name, int cuisineId)
        {
            var reqModel = new AddRestaurantRequestModel
            {
                Name = name,
                CuisineId = cuisineId,
                UserId = _identity.GetRequestingUserId()
            };
            return new AddRestaurantTransaction(_repo, _log, reqModel);
        }

        public PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(int restaurantId, string name, int cuisineId)
        {
            var reqModel = CreateUpdateRestaurantRequestModel(restaurantId, name, cuisineId);
            return new PartialUpdateRestaurantTransaction(_repo, _log, reqModel);
        }

        private UpdateRestaurantRequestModel CreateUpdateRestaurantRequestModel(int restaurantId, string name, int cuisineId)
        {
            var reqModel = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                Name = name,
                CuisineId = cuisineId,
                UserId = _identity.GetRequestingUserId()
            };
            return reqModel;
        }

        public CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(int restaurantId, string name, int cuisineId)
        {
            var reqModel = CreateUpdateRestaurantRequestModel(restaurantId, name, cuisineId);
            return new CompleteUpdateRestaurantTransaction(_repo, _log, reqModel);
        }


        public ViewReviewsForRestaurantTransaction CreateViewReviewsForRestaurantTransaction(int restaurantId)
        {
            var reqModel = new ViewRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                UserId = _identity.GetRequestingUserId()
            };
            return new ViewReviewsForRestaurantTransaction(_repo, _log, reqModel);
        }

        public ViewCuisinesTransaction CreateViewAllCuisinesTransaction()
        {
            var reqModel = new ViewCuisinesRequestModel { UserId = _identity.GetRequestingUserId() };
            return new ViewCuisinesTransaction(_repo, _log, reqModel);
        }

        public AddReviewTransaction CreateAddReviewsForRestaurantTransaction(int restaurantId, Review reviewRequest)
        {
            var reqModel = new AddReviewRequestModel
            {
                RestaurantId = restaurantId,

                Comment = reviewRequest.Comment,
                DateTimePosted = reviewRequest.PostedDateTime,
                Rating = reviewRequest.Rating,
                UserId = _identity.GetRequestingUserId()
            };
            return new AddReviewTransaction(_repo, _log, reqModel);
        }

        public RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove)
        {
            var reqModel = new RemoveRestaurantRequestModel
            {
                RestaurantId = restaurantIdToRemove,
                UserId = _identity.GetRequestingUserId()
            };
            return new RemoveRestaurantTransaction(_repo, _log, reqModel);
        }

        public ViewAllRestaurantsTransaction CreateViewAllRestaurantsTransaction()
        {
            var reqModel = new ViewAllRestaurantRequestModel { UserId = _identity.GetRequestingUserId() };
            return new ViewAllRestaurantsTransaction(_repo, _log, reqModel);
        }
    }
}