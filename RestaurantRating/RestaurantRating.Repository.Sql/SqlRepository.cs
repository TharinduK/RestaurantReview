using RestaurantRating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var cuisineList = from cuisine in _restaurantDbContex.Cuisines
                select
                (new Domain.Cuisine
                {
                    Id = cuisine.Id,
                    CreatedBy = cuisine.CreatedBy,
                    Name = cuisine.Name,
                    UpdatedBy = cuisine.UpdatedBy
                });
            return cuisineList.ToList();
        }

        public bool DoseUserIdAlreadyExist(int requestUserId)
        {
            throw new NotImplementedException();
        }

        public object GetAdminUser()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int userId)
        {
            var appUser = _restaurantDbContex.AppUsers.FirstOrDefault<Sql.AppUser>(u => u.Id == userId);
            if (appUser != null)
                return new Domain.User
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                };
            else
                return null;
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
                .Select(
                    r =>
                        new Domain.Restaurant
                        {
                            Id = r.Id,
                            Cuisine = new Domain.Cuisine {Id = r.CuisineId},
                            Name = r.Name,
                            CreatedBy = r.CreatedBy,
                            UpdatedBy = r.UpdatedBy
                        });

            return cuisineRestList.ToList();
        }

        public Domain.Restaurant GetRestaurantById(int restaurantId)
        {
            throw new NotImplementedException();
            //var restarentFound = _restaurantDbContex.Restaurants.FirstOrDefault<Sql.Restaurant>(r => r.Id == restaurantId);
            //if (restarentFound != null)
            //    return new Domain.Restaurant
            //    {
            //        Id = appUser.Id,
            //        UserName = appUser.UserName,
            //        FirstName = appUser.FirstName,
            //        LastName = appUser.LastName,
            //    };
            //else
            //    return null;
        }

        public Domain.Restaurant GetRestaurantWithReviewsById(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Restaurant> GetAllRestaurantsWithReview()
        {
            throw new NotImplementedException();
        }

        public int AddRestaurentGetNewId(AddRestaurantRequestModel requestModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateRestaurant(UpdateRestaurantRequestModel request)
        {
            throw new NotImplementedException();
        }

        public void RemoveRestaurentId(RemoveRestaurantRequestModel reqeustModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Review> GetReviewsForRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public int AddReviewGetNewId(AddReviewRequestModel reviewToAdd)
        {
            throw new NotImplementedException();
        }

        public bool DoseCuisineIdExist(int requestCusineId)
        {
            return _restaurantDbContex.Cuisines.Any(c => c.Id == requestCusineId);
        }

 

        public Domain.Cuisine GetCuisineById(int cuisineId)
        {
            throw new NotImplementedException();
        }
    }
}
