using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestaurantRating.API.Controllers
{
    [EnableCors("*", "*", "GET,POST")]
    [RoutePrefix("api")]
    public class ReviewsController : ControllerBase
    {
        public ReviewsController(IRepository repo, IApplicationLog logger, ITransactionFactory factory) 
            : base(repo, logger, factory){}

        public ReviewsController(){}

        [HttpGet]
        [Route("Restaurants/{id}/Reviews", Name ="ReviewsForRestaurant")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tran = Factory.CreateViewReviewsForRestaurantTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var reviews = ViewModelMapper.ConvertDomainReviewToViewModel(tran.Response.Reviews);
                    return Ok(reviews); 
                }
                else return BadRequest(); //400
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError(); //500
            }
        }

        [HttpPost]
        [Route("Restaurants/{id}/Reviews", Name = "NewReviewForRestaurant")]
        public IHttpActionResult Post(int id, [FromBody] ViewModels.Review reviewRequest)
        {
            try
            {
                AddReviewTransaction tran = Factory.CreateAddReviewsForRestaurantTransaction(id, reviewRequest); 
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    reviewRequest.ReviewNumber = tran.Response.ReviewNumber; //TK: check if the user name needs to be returned 
                    return CreatedAtRoute("NewReviewForRestaurant", new { id = id}, reviewRequest);
                }
                else return BadRequest(); 
            }
            catch (UserNotFoundException) { return BadRequest();}
            catch (RestaurantNotFoundException) { return NotFound(); } 
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError();
            }
        }
    }
}
