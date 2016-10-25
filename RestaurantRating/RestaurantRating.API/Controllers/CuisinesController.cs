using System;
using System.Web.Http;
using System.Web.Http.Cors;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;

namespace RestaurantRating.API.Controllers
{
    [EnableCors("*", "*", "GET")]
    [RoutePrefix("api")]
    public class CuisinesController : ControllerBase
    {
        //public CuisinesController(IRepository repo, IApplicationLog logger, ITransactionFactory factory) 
        //    : base(repo, logger, factory)
        //{
        //}
        public CuisinesController(IRepository repo, IApplicationLog logger)
            : base(repo, logger){}

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var tran = Factory.CreateViewAllCuisinesTransaction();
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var allCuisines = ViewModelMapper.ConvertDomainCuisineToViewModel(tran);
                    return Ok(allCuisines);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                Logger.ErrorLog("Unable to fetch valid restaurants", ex);
                return InternalServerError();
            }
        }

        [Route("Cuisines/{id}/restaurants", Name = "RestaurantsForCuisine")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var tran = Factory.CreateViewRestaurantsForCuisineTransaction(id);
                tran.Execute();

                if (tran.Response.WasSucessfull)
                {
                    var rest = ViewModelMapper.ConvertDomainRestaurantGroupToViewModel(tran.Response.Restaurants);
                    return Ok(rest);
                }
                return BadRequest();
            }
            catch (CuisineNotFoundException) { return NotFound(); }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Web API failed getting restaurant id {id}", ex);
                return InternalServerError();
            }
        }

    }
}
