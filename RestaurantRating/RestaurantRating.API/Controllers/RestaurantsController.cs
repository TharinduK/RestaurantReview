using System;
using System.Net;
using System.Web.Http;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;

namespace RestaurantRating.API
{
    public class RestaurantsController : ControllerBase
    {
        public RestaurantsController(IRepository repo, IApplicationLog logger, ITransactionFactory factory) 
            : base(repo, logger, factory){}

        public RestaurantsController(){}

        // GET api/Restaurants
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var tran = Factory.CreateViewAllRestaurantsTransaction();
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var allRestaurants = ViewModelMapper.ConvertDomainRestaurantGroupToViewModel(tran.Response.Restaurants);
                    return Ok(allRestaurants);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("Unable to fetch valid restaurants", ex);
                return InternalServerError();
            }
        }

        // Read: api/Restaurants/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tran = Factory.CreateViewRestaurantTransaction(id);
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
                Logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
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

                var tran = Factory.CreateAddRestraurantTransaction(restaurantRequest.Name, restaurantRequest.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    restaurantRequest.Id = tran.Response.RestaurantId;
                    restaurantRequest.CuisineName = tran.Response.CuisineName;
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
                Logger.ErrorLog($"Web API failed add new restaurant {restaurantRequest}", ex);
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

                var tran = Factory.CreateCompleteUpdateRestraurantTransaction(id, restaurant.Name, restaurant.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); //200
                else return BadRequest();

            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); //500
            }
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]ViewModels.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) return BadRequest();//400

                var tran = Factory.CreatePartialUpdateRestraurantTransaction(id, restaurant.Name, restaurant.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); //200
                else return BadRequest();
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); //500
            }
        }


        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var tran = Factory.CreateDeleteRestraurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull) return StatusCode(HttpStatusCode.NoContent); //204
                else return BadRequest(); //400

            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {id}", ex);
                return InternalServerError(); //500
            }
        }
    }
}