using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using RestaurantRating.Domain;

namespace RestaurantRating.API
{
    [EnableCors("*", "*", "*")]
    public class RestaurantsController : ControllerBase
    {
        public RestaurantsController(IApplicationLog logger, ITransactionFactory factory)
            : base(logger, factory) { }

        public RestaurantsController(IRepository repo, IApplicationLog logger)
            : base(repo, logger) { }

        // api/Restaurants
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

        // api/Restaurants/5
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

        // api/Restaurants
        [HttpPost]
        public IHttpActionResult Post([FromBody] ViewModels.Restaurant restaurantRequest)
        {
            try
            {
                if (restaurantRequest == null) return BadRequest(); 

                var tran = Factory.CreateAddRestraurantTransaction(restaurantRequest.Name, restaurantRequest.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    restaurantRequest.Id = tran.Response.RestaurantId;
                    restaurantRequest.CuisineName = tran.Response.CuisineName;
                    return CreatedAtRoute("DefaultRouting", new { id = restaurantRequest.Id }, restaurantRequest);
                }
                else return BadRequest();
            }
            catch (RestaurantAlreadyExistsException) { return BadRequest(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {restaurantRequest}", ex);
                return InternalServerError();
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]ViewModels.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) return BadRequest(); 

                var tran = Factory.CreateCompleteUpdateRestraurantTransaction(id, restaurant.Name, restaurant.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); 
                else return BadRequest();

            }
            catch (RestaurantNotFoundException) { return NotFound(); } 
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); 
            }
        }

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]ViewModels.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) return BadRequest();

                var tran = Factory.CreatePartialUpdateRestraurantTransaction(id, restaurant.Name, restaurant.CuisineId);
                tran.Execute();

                if (tran.Response.WasSucessfull) return Ok(new ViewModels.Restaurant {Id = id}); 
                else return BadRequest();
            }
            catch (RestaurantNotFoundException) { return NotFound(); } 
            catch (RestaurantInvalidInputException) { return BadRequest(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {restaurant}", ex);
                return InternalServerError(); 
            }
        }


        // api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var tran = Factory.CreateDeleteRestraurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull) return StatusCode(HttpStatusCode.NoContent); 
                else return BadRequest(); 

            }
            catch (RestaurantNotFoundException) { return NotFound(); } 
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed add new restaurant {id}", ex);
                return InternalServerError(); 
            }
        }
    }
}