using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using RestaurantRating.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RestaurantRating.API.Controllers
{
    [RoutePrefix("api")]
    public class ReviewsController : ApiController
    {
        private readonly IApplicationLog _logger;
        private readonly TransactionFactory _factory;

        public ReviewsController(IRepository repo, IApplicationLog logger)
        {
            _logger = logger;
            _factory = new TransactionFactory(repo, _logger, 1);
#warning userID hardcoded  (must use factory inteface)
        }

        public ReviewsController()
        {
            //todo: must be removed with di container 
            IRepository repository = new InMemoryRepository();
            _logger = new InMemoryApplicationLog();
            _factory = new TransactionFactory(repository, _logger, 1);
        }

        [HttpGet]
        [Route("Restaurants/{restaurantId}/Reviews", Name ="ReviewsForRestaurant")]
        public IHttpActionResult Get(int restaurantId)
        {
            try
            {
                var tran = _factory.CreateViewReviewsForRestaurantTransaction(restaurantId);
                tran.Execute();

                var reviews = ConvertToReviewViewModel(tran);
                if (tran.Response.WasSucessfull) return Ok(reviews); //200
                return BadRequest(); //400
            }
            catch (RestaurantNotFoundException) { return NotFound(); } //404
            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed getting restaurant id {restaurantId}", ex);
                return InternalServerError(); //500
            }
        }

        //[HttpGet]
        //[Route("Reviews/{reviewId}", Name = "ReviewsForRestaurant")]
        //public IHttpActionResult Get(int reviewid)
        //{
        //    try
        //    {
        //        var tran = _factory.CreateViewReviewsForRestaurantTransaction(reviewid);
        //        tran.Execute();

        //        var reviews = ConvertToReviewViewModel(tran);
        //        if (tran.Response.WasSucessfull) return Ok(reviews); //200
        //        return BadRequest(); //400
        //    }
        //    catch (RestaurantNotFoundException) { return NotFound(); } //404
        //    catch (Exception ex)
        //    {
        //        _logger.ErrorLog($"Web API failed getting restaurant id {reviewid}", ex);
        //        return InternalServerError(); //500
        //    }
        //}

        private static List<ViewModels.Review> ConvertToReviewViewModel(ViewReviewsForRestaurantTransaction tran)
        {
            List<ViewModels.Review> reviews = new List<ViewModels.Review>();
            foreach (var review in tran.Response.Reviews)
                reviews.Add(new ViewModels.Review
                {
                    Comment = review.Comment,
                    PostedDateTime = review.PostedDateTime,
                    Rating = review.Rating,
                    ReviewNumber = review.ReviewNumber,
                    UserName = review.ReviewUser.UserName
                });
#warning add usernamen prop to review suer object 
            return reviews;
        }

        //public IHttpActionResult Post()
    }
}
