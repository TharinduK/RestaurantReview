using RestaurantRating.Domain;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestaurantRating.API.Controllers
{
    [EnableCors("*", "*", "GET,POST")]
    [RoutePrefix("api")]
    public class ReviewsController : ControllerBase
    {
        //public ReviewsController(IApplicationLog logger, ITransactionFactory factory)
        //    : base(logger, factory) { }

        public ReviewsController(IRepository repo, IApplicationLog logger)
        : base(repo, logger) { }

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
                else return BadRequest(); 
            }
            catch (RestaurantNotFoundException) { return NotFound(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError(); 
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
                    reviewRequest.ReviewNumber = tran.Response.ReviewNumber; 
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
