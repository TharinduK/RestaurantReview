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
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tran = _factory.CreateViewRestaurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(tran.Response); //200
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError(); //500
            }
        }

        [HttpPost]
        // POST api/<controller>
        public IHttpActionResult Post([FromBody] AddRestaurantRequestModel value)
        {
            try
            {
                if (value == null) return BadRequest();

                var tran = _factory.CreateAddRestraurantTransaction(value);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    return Created(Request.RequestUri + "/" + tran.Response.RestaurantId, tran.Response); //201
                }
            return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {value}", ex);
                return InternalServerError(); //500
            }
        }

        //TODO: put must update all the fields 

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]UpdateRestaurantRequestModel value)
        {
            try
            {
                if (value == null) return BadRequest();

                var tran = _factory.CreateUpdateRestraurantTransaction(value);
                tran.Execute();

                //Todo: must diferenciate not found, bad reqeust
                if (tran.Response.WasSucessfull)
                {
                    return Ok(tran.Response); //200
                }
                //not found
                return NotFound();
                //bad request
                //return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed add new restaurant {value}", ex);
                return InternalServerError(); //500
            }
        }

        //todo: use patch for partial updates
        //jason patch RFC6902 standard https://tools.ietf.org/html/rfc6902
        //use Marvin.JasonPatch == to handel patch operation 
        //patch must use Content-type:application/jeson-patch+json


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}