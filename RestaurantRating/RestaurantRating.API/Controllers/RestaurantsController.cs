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
        private readonly TransactionFactory _factory;

        public RestaurantsController(IRepository repo, IApplicationLog logger)
        {
            _logger = logger;
            _factory = new TransactionFactory(repo, _logger, 1);
#warning userID hardcoded  (must use factory inteface)
        }

        public RestaurantsController()
        {
            //todo: must be removed with di container 
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

                if (tran.Response.WasSucessfull) return Ok(tran.Response); //200
                else return BadRequest();//400
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

                if (tran.Response.WasSucessfull) return Ok(tran.Response); //200
                return BadRequest(); //400
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError(); //500
            }
        }

        // create: api/Restaurants
        [HttpPost]
        public IHttpActionResult Post([FromBody] AddRestaurantRequestModel value)
        {
            try
            {
                if (value == null) return BadRequest(); //400

                var tran = _factory.CreateAddRestraurantTransaction(value);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    return Created(Request.RequestUri + "/" + tran.Response.RestaurantId, tran.Response); //201
                }
                else
                {
                    return BadRequest(); //400 -- for PK violations, we would send a bad request response 
                }
            }
            catch (RestaurantAlreadyExistsException){return BadRequest(); }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {value}", ex);
                return InternalServerError(); //500
            }
        }

        //TODO: put must update all the fields 

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]UpdateRestaurantRequestModel value)
        {
            try
            {
                if (value == null) return BadRequest(); //400
                var tran = _factory.CreateCompleteUpdateRestraurantTransaction(value);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    return Ok(tran.Response); //200
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (RestaurantNotFoundException){return NotFound(); } //404
            catch (RestaurantInvalidInputException){return BadRequest(); }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {value}", ex);
                return InternalServerError(); //500
            }
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]UpdateRestaurantRequestModel value)
        {
            try
            {
                if (value == null) return BadRequest();//400

                var tran = _factory.CreatePartialUpdateRestraurantTransaction(value);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    return Ok(tran.Response); //200
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (RestaurantInvalidInputException) { return BadRequest(); } 
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {value}", ex);
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

                if (tran.Response.WasSucessfull)
                {
                    return StatusCode(HttpStatusCode.NoContent);//204
                }
                else
                {
                    return BadRequest();//400
                }
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