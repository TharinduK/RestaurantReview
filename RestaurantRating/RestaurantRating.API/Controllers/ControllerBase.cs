using System.Web.Http;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;

namespace RestaurantRating.API
{
    public class ControllerBase : ApiController
    {
        protected IApplicationLog Logger;
        protected ITransactionFactory Factory;

        public ControllerBase(IRepository repo, IApplicationLog logger, ITransactionFactory factory)
        {
            Logger = logger;
            Factory = factory;
#warning userID hardcoded  (must use factory inteface)
        }

        public ControllerBase()
        {
            //todo: must be removed with di container 
            IRepository repository = new InMemoryRepository();
            Logger = new InMemoryApplicationLog();
            Factory = new TransactionFactory(repository, Logger, 1);
        }
    }
}