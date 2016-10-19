using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;

namespace RestaurantRating.API
{
    public class RestaurantsController : ApiController
    {
        private IRepository _repository;
        private readonly IApplicationLog _logger;
        private TransactionFactory _factory;

        public RestaurantsController(IRepository repo, IApplicationLog logger)
        {
            _repository = repo;
            _logger = logger;
            _factory = new TransactionFactory(_repository, _logger, 1);
#warning userID hardcoded  (must use factory inteface)
        }

        public RestaurantsController()
        {
            //todo: must be removed with di container 
            _repository = new InMemoryRepository();
            _logger = new InMemoryApplicationLog();
            _factory = new TransactionFactory(_repository, _logger, 1);
        }

        // GET api/<controller>
        public IEnumerable<Restaurant> Get()
        {
            return new Restaurant[] { new Restaurant() };
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
                if (value == null) return BadRequest();//400

                var tran = _factory.CreateAddRestraurantTransaction(value);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    return Created(Request.RequestUri + "/" + tran.Response.RestaurantId, tran.Response); //201
                }
                else
                {
                    return BadRequest();//400 -- for PK violations, we would send a bad request response 
                }
            }
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
                if (value == null) return BadRequest();//400
                //update PUT to include full element/resaurce (PUT only for full entity updates)
                var tran = _factory.CreateUpdateRestraurantTransaction(value);
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

                var tran = _factory.CreateUpdateRestraurantTransaction(value);
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