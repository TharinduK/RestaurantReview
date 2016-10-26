using RestaurantRating.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestaurantRating.Repository.Sql
{
    public class SqlRepository : IRepository
    {
        private readonly RestaurantEntities _restaurantDbContex;

        public SqlRepository(RestaurantEntities db)
        {
            _restaurantDbContex = db;
        }

        public IEnumerable<Domain.Cuisine> GetAllCuisines()
        {
            var cuisineList = new List<Domain.Cuisine>();
                
            foreach(var cuisine in _restaurantDbContex.Cuisines) cuisineList.Add(DomainFactory.CreateCuisine(cuisine));

            return cuisineList;
        }

        public bool DoseUserIdAlreadyExist(int requestUserId)
        {
            return _restaurantDbContex.AppUsers.Any<AppUser>(u => u.Id == requestUserId);
        }

        public object GetAdminUser()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            var appUser = _restaurantDbContex.AppUsers.FirstOrDefault<Sql.AppUser>(u => u.Id == userId);

            return DomainFactory.CreateUser(appUser);
        }

        public bool DoseRestaurentNameAlreadyExist(string restaurantNameToCheck)
        {
            return _restaurantDbContex.Restaurants.Any(r => r.Name == restaurantNameToCheck);
        }

        public bool DoseRestaurentIdExist(int restaurantId)
        {
            return _restaurantDbContex.Restaurants.Any(r => r.Id == restaurantId);
        }

        public IEnumerable<Domain.Restaurant> GetRestaurantForCuisine(int requestCusineId)
        {
            var cuisineRestList = _restaurantDbContex.Restaurants
                .Where(r => r.CuisineId == requestCusineId)
                .Include(r => r.Cuisine);

            var returnRestList = new List<Domain.Restaurant>();
            foreach (var rest in cuisineRestList) returnRestList.Add(DomainFactory.CreateRestaurant(rest));

            return returnRestList;
        }

        public Domain.Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurantFound = _restaurantDbContex.Restaurants
                .Include(r => r.Cuisine)
                .FirstOrDefault<Sql.Restaurant>(r => r.Id == restaurantId);
            return DomainFactory.CreateRestaurant(restaurantFound);
        }

        public Domain.Restaurant GetRestaurantWithReviewsById(int restaurantId)
        {
            var restaurantFound = _restaurantDbContex.Restaurants
                .Where(r => r.Id == restaurantId)
                .Include(r => r.Reviews.Select(rev => rev.AppUser))
                .Include(r => r.Cuisine)
                .FirstOrDefault<Sql.Restaurant>();
            return DomainFactory.CreateRestaurantWithReivew(restaurantFound);
        }

        public IEnumerable<Domain.Restaurant> GetAllRestaurantsWithReview()
        {
            var allrestaurants = _restaurantDbContex.Restaurants
                .Include(r => r.Reviews.Select(rev => rev.AppUser))
                .Include(r => r.Cuisine);

            var restaurantsToReturn = new List<Domain.Restaurant>();
            foreach(var rest in allrestaurants) restaurantsToReturn.Add(DomainFactory.CreateRestaurantWithReivew(rest));

            return restaurantsToReturn;
        }

        public int AddRestaurentGetNewId(AddRestaurantRequestModel requestModel)
        {
            var newRestaurant = new Sql.Restaurant
            {
                Name = requestModel.Name,
                CuisineId = requestModel.CuisineId,
                CreatedBy = requestModel.UserId,
                UpdatedBy = requestModel.UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _restaurantDbContex.Restaurants.Add(newRestaurant);
            _restaurantDbContex.SaveChanges();
            return newRestaurant.Id;
        }

        public void UpdateRestaurant(UpdateRestaurantRequestModel request)
        {
            var restaurantFound = _restaurantDbContex.Restaurants.FirstOrDefault<Sql.Restaurant>(r => r.Id == request.RestaurantId);
            if (restaurantFound != null)
            {
                restaurantFound.CuisineId = request.CuisineId;
                restaurantFound.Name = request.Name;
                restaurantFound.UpdatedBy = request.UserId;
                restaurantFound.UpdatedDate = DateTime.Now;

                _restaurantDbContex.SaveChanges();
            }
        }

        public void RemoveRestaurentId(RemoveRestaurantRequestModel request)
        {
            var restaurantFound = _restaurantDbContex.Restaurants.FirstOrDefault<Sql.Restaurant>(r => r.Id == request.RestaurantId);
            if (restaurantFound != null)
            {
                _restaurantDbContex.Restaurants.Remove(restaurantFound);
                _restaurantDbContex.SaveChanges();
            }
        }

        public IEnumerable<Domain.Review> GetReviewsForRestaurant(int restaurantId)
        {
            var reviewsFound = _restaurantDbContex.Reviews
                .Where(r => r.RestaurantId == restaurantId)
                .Include(r => r.AppUser);

            var reviewToReturn = new List<Domain.Review>();
            foreach (var r in reviewsFound) reviewToReturn.Add(DomainFactory.CreateReview(r));

            return reviewToReturn;
        }

        public int AddReviewGetNewId(AddReviewRequestModel reviewToAdd)
        {
            var newReview = new Sql.Review
            {
                Comment = reviewToAdd.Comment,
                CreatedBy = reviewToAdd.UserId,
                UpdatedBy = reviewToAdd.UserId,
                Rating = reviewToAdd.Rating,
                RestaurantId = reviewToAdd.RestaurantId,
                PostedDate = reviewToAdd.DateTimePosted,
                ReviewUser = reviewToAdd.UserId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            _restaurantDbContex.Reviews.Add(newReview);
            _restaurantDbContex.SaveChanges();
            return newReview.ReviewNumber;
        }

        public bool DoseCuisineIdExist(int requestCusineId)
        {
            return _restaurantDbContex.Cuisines.Any(c => c.Id == requestCusineId);
        }

        public Domain.Cuisine GetCuisineById(int cuisineId)
        {
            var cuisineFound = _restaurantDbContex.Cuisines
                .FirstOrDefault<Sql.Cuisine>(c => c.Id == cuisineId);
            return DomainFactory.CreateCuisine(cuisineFound);
        }
    }
}
