using System;
using System.Net;
using System.Web.Http;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;

namespace RestaurantRating.API
{
    public class RestaurantsController : ApiController
    {
        private readonly IApplicationLog _logger;
        private readonly ITransactionFactory _factory;
        private readonly ViewModelMapper _viewModelMapper;

        public RestaurantsController(IRepository repo, IApplicationLog logger, ITransactionFactory factory)
        {
            _viewModelMapper = new ViewModelMapper();
            _logger = logger;
            _factory = factory;
#warning userID hardcoded  (must use factory inteface)
        }

        public RestaurantsController()
        {
            //todo: must be removed with di container 
            _viewModelMapper = new ViewModelMapper();
            IRepository repository = new InMemoryRepository();
            _logger = new InMemoryApplicationLog();
            _factory = new TransactionFactory(repository, _logger, 1);
        }

        // GET api/Restaurants
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var tran = _factory.CreateViewAllRestaurantsTransaction();
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var allRestaurants = _viewModelMapper.ConvertDomainRestaurantToViewModel(tran);
                    return Ok(allRestaurants);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog("Unable to fetch valid restaurants", ex);
                return InternalServerError();
            }
        }

        // Read: api/Restaurants/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tran = _factory.CreateViewRestaurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var rest = ViewModelMapper.ConvertDomainRestaurantToViewModel(id, tran);
                    return Ok(rest);
                }
                return BadRequest();
            }
            catch (RestaurantNotFoundException) { return NotFound(); } 
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError();
            }
        }

        // create: api/Restaurants
        [HttpPost]
        public IHttpActionResult Post([FromBody] ViewModels.Restaurant restaurantRequest)
        {
            try
            {
                if (restaurantRequest == null) return BadRequest(); //400

                var tran = _factory.CreateAddRestraurantTransaction(restaurantRequest.Name, restaurantRequest.Cuisine);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    restaurantRequest.Id = tran.Response.RestaurantId;
                    return CreatedAtRoute("DefaultRouting",new {id = restaurantRequest.Id }, restaurantRequest); //201
                }
                else
                {
                    return BadRequest(); //400 -- for PK violations, we would send a bad request response 
                }
            }
            catch (RestaurantAlreadyExistsException) { return BadRequest(); }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {restaurantRequest}", ex);
                return InternalServerError(); //500
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ViewModels.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) return BadRequest(); //400

                var tran = _factory.CreateCompleteUpdateRestraurantTransaction(id, restaurant.Name, restaurant.Cuisine);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); //200
                else return BadRequest();

            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); //500
            }
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]ViewModels.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) return BadRequest();//400

                var tran = _factory.CreatePartialUpdateRestraurantTransaction(id, restaurant.Name, restaurant.Cuisine);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); //200
                else return BadRequest();
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); //500
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var tran = _factory.CreateDeleteRestraurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull) return StatusCode(HttpStatusCode.NoContent); //204
                else return BadRequest(); //400

            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {id}", ex);
                return InternalServerError(); //500
            }
        }
    }
}