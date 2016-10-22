using RestaurantRating.Domain;

namespace RestaurantRating.API.Factories
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly IRepository _repo;
        private readonly IApplicationLog _log;
        private readonly int _callingUserId;

        public TransactionFactory(IRepository repo, IApplicationLog log, int callingUserId)
        {
            _repo = repo;
            _log = log;
            _callingUserId = callingUserId;
        }
        public ViewRestaurantTransaction CreateViewRestaurantTransaction(int restaurantId)
        {
            var reqModel = new ViewRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                UserId = _callingUserId
            };
            return new ViewRestaurantTransaction(_repo, _log, reqModel);
        }

        public AddRestaurantTransaction CreateAddRestraurantTransaction(string name, string cuisine)
        {
            var reqModel = new AddRestaurantRequestModel
            {
                Name = name,
                Cuisine = cuisine,
                UserId = _callingUserId
            };
            return new AddRestaurantTransaction(_repo, _log, reqModel);
        }

        public PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(int restaurantId, string name, string cuisine)
        {
            var reqModel = CreateUpdateRestaurantRequestModel(restaurantId, name, cuisine);
            return new PartialUpdateRestaurantTransaction(_repo, _log, reqModel);
        }

        private UpdateRestaurantRequestModel CreateUpdateRestaurantRequestModel(int restaurantId, string name, string cuisine)
        {
            var reqModel = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                Name = name,
                Cuisine = cuisine,
                UserId = _callingUserId
            };
            return reqModel;
        }

        public CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(int restaurantId, string name, string cuisine)
        {
            var reqModel = CreateUpdateRestaurantRequestModel(restaurantId, name, cuisine);
            return new CompleteUpdateRestaurantTransaction(_repo, _log, reqModel);
        }


        public ViewReviewsForRestaurantTransaction CreateViewReviewsForRestaurantTransaction(int restaurantId)
        {
            var reqModel = new ViewRestaurantRequestModel
            {
                RestaurantId = restaurantId,
                UserId = _callingUserId
            };
            return new ViewReviewsForRestaurantTransaction(_repo, _log, reqModel);
        }


        public RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove)
        {
            var reqModel = new RemoveRestaurantRequestModel
            {
                RestaurantId = restaurantIdToRemove,
                UserId = _callingUserId
            };
            return new RemoveRestaurantTransaction(_repo, _log, reqModel);
        }

        public ViewAllRestaurantsTransaction CreateViewAllRestaurantsTransaction()
        {
            var reqModel = new ViewAllRestaurantRequestModel {UserId = _callingUserId};
            return new ViewAllRestaurantsTransaction(_repo, _log, reqModel);
        }
    }
}