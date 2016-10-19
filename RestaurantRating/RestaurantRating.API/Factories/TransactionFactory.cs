using RestaurantRating.Domain;

namespace RestaurantRating.API.Factories
{
    public class TransactionFactory
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

        internal AddRestaurantTransaction CreateAddRestraurantTransaction(AddRestaurantRequestModel value)
        {
            return new AddRestaurantTransaction(_repo, _log, value);
        }

        internal PartialUpdateRestaurantTransaction CreatePartialUpdateRestraurantTransaction(UpdateRestaurantRequestModel value)
        {
            return new PartialUpdateRestaurantTransaction(_repo, _log, value);
        }

        internal CompleteUpdateRestaurantTransaction CreateCompleteUpdateRestraurantTransaction(UpdateRestaurantRequestModel value)
        {
            return new CompleteUpdateRestaurantTransaction(_repo, _log, value);
        }

        internal RemoveRestaurantTransaction CreateDeleteRestraurantTransaction(int restaurantIdToRemove)
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