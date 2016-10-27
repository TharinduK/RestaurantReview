using System.Web.Http;
using RestaurantRating.Domain;

namespace RestaurantRating.API
{
    public class ControllerBase : ApiController
    {
        protected IApplicationLog Logger;
        protected ITransactionFactory Factory;

        public ControllerBase(IRepository repo, IApplicationLog logger, IIdentityProvider identity)
        {
            Logger = logger;
            Factory = new TransactionFactory(repo, logger, identity);
        }
    }
}