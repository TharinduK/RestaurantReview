using System.Web.Http;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;

namespace RestaurantRating.API
{
    public class ControllerBase : ApiController
    {
        protected IApplicationLog Logger;
        protected ITransactionFactory Factory;

//        public ControllerBase(IRepository repo, IApplicationLog logger, ITransactionFactory factory)
//        {
//            Logger = logger;
//            Factory = factory;
//#warning userID hardcoded  (must use factory inteface)
//        }

        public ControllerBase(IRepository repo, IApplicationLog logger)
        {
#warning userID hardcoded  (must use factory inteface)
            Logger = logger;
            Factory = new TransactionFactory(repo, logger, 1);
        }
    }
}