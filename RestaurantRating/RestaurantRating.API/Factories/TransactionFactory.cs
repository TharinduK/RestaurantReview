using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public AddRestaurantTransaction CreateAddRestraurantTransaction(AddRestaurantRequestModel value)
        {
            return new AddRestaurantTransaction(_repo, _log, value);
        }

        public UpdateRestaurantTransaction CreateUpdateRestraurantTransaction(UpdateRestaurantRequestModel value)
        {
            return new UpdateRestaurantTransaction(_repo, _log, value);
        }
    }
}