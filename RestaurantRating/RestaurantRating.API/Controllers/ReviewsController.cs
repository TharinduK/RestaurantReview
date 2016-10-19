using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantRating.API.Controllers
{
    public class ReviewsController : ApiController
    {
        private IRepository _repository;
        private readonly IApplicationLog _logger;
        private TransactionFactory _factory;

        public ReviewsController(IRepository repo, IApplicationLog logger)
        {
            _repository = repo;
            _logger = logger;
            _factory = new TransactionFactory(_repository, _logger, 1);
#warning userID hardcoded  (must use factory inteface)
        }

        public ReviewsController()
        {
            //todo: must be removed with di container 
            _repository = new InMemoryRepository();
            _logger = new InMemoryApplicationLog();
            _factory = new TransactionFactory(_repository, _logger, 1);
        }

        [HttpGet]
        [Route("/api/Restaurants/{restaurantId}/Reviews")]
        public IHttpActionResult Get(int restaurantId)
        {
            try
            {
                var tran = _factory.CreateViewRestaurantTransaction(restaurantId);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(tran.Response); //200
                return BadRequest(); //400
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed getting restaurant id {restaurantId}", ex);
                return InternalServerError(); //500
            }
        }
    }
}
