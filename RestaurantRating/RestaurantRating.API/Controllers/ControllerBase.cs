using System.Web.Http;
using RestaurantRating.Domain;

namespace RestaurantRating.API
{
    public class ControllerBase : ApiController
    {
        protected IApplicationLog Logger;
        protected ITransactionFactory Factory;

        //public ControllerBase(IApplicationLog logger, ITransactionFactory factory)
        //{
        //    Logger = logger;
        //    Factory = factory;
        //}

        public ControllerBase(IRepository repo, IApplicationLog logger)
        {
            //TODO: Get Authentication Server and pass in user ID
            Logger = logger;
            Factory = new TransactionFactory(repo, logger, 1);
        }
    }
}