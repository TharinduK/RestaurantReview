using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantRating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace RestaurantRating.DomainTests
{
    public class MockTestSetup
    {
        protected IRepository Repo;
        protected List<Restaurant> Restaurants = new List<Restaurant>();
        protected List<Review> Reviews = new EditableList<Review>();
        protected List<User> Users = new List<User>();
        protected List<Cuisine> Cuisines = new List<Cuisine>();

        protected IApplicationLog Log;

        [TestInitialize]
        public void SetUp()
        {
            var log = new Mock<IApplicationLog>();
            Log = log.Object;

            var repo = new Mock<IRepository>();
            repo.Setup(m => m.AddRestaurentGetNewId(It.IsAny<AddRestaurantRequestModel>()))
                .Returns<AddRestaurantRequestModel>(r => FakeAddRestaurant(r));
                        
            repo.Setup(m => m.DoseRestaurentNameAlreadyExist(It.IsAny<string>()))
                .Returns<string>(r => FakeResturantExisits(r));

            repo.Setup(m => m.DoseRestaurentIdAlreadyExist(It.IsAny<int>()))
                .Returns<int>(id => FakeResturantIdExisits(id));

            repo.Setup(m => m.RemoveRestaurentId(It.IsAny<RemoveRestaurantRequestModel>()))
                .Callback<RemoveRestaurantRequestModel>(r => FakeRemoveRestaurant(r));

            repo.Setup(m => m.GetRestaurantById(It.IsAny<int>()))
                .Returns<int>(id => FakeGetRestaurantById(id));

            repo.Setup(m => m.GetRestaurantWithReviewsById(It.IsAny<int>()))
                .Returns<int>(r => FakeGetRestaurantWithReviewesById(r));

            repo.Setup(m => m.GetReviewsForRestaurant(It.IsAny<int>()))
                .Returns<int>(r => FakeGetReviewsForRestaurant(r));

            repo.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Returns<AddReviewRequestModel>(r => FakeAddReview(r));

            repo.Setup(m => m.GetUserById(It.IsAny<int>()))
                .Returns<int>(id => Users[id - 1]);

            repo.Setup(m => m.DoseUserIdAlreadyExist(It.IsAny<int>()))
                .Returns<int>(id => id <= Users.Count && id > 0);

            repo.Setup(m => m.UpdateRestaurant(It.IsAny<UpdateRestaurantRequestModel>()))
                .Callback<UpdateRestaurantRequestModel>(r => FakeUpdateRestaurant(r));

            repo.Setup(m => m.GetCuisineById(It.IsAny<int>()))
                .Returns<int>(id => Cuisines[id]);

            repo.Setup(m => m.DoseCuisineIdExist(It.IsAny<int>()))
                .Returns<int>(id => (id > 0 && id <= Cuisines.Count));

            repo.Setup(m => m.GetAllRestaurantsWithReview())
                .Returns(FakeGetAllRestaurants());

            repo.Setup(m => m.GetAllCuisines())
                .Returns(Cuisines);

            repo.Setup(m => m.GetRestaurantForCuisine(It.IsAny<int>()))
                .Returns<int>(id => FakeGetRestaurantsForCuisineID(id));
                
            Repo = repo.Object;
        }

        private IEnumerable<Restaurant> FakeGetRestaurantsForCuisineID(int id)
        {
            var foundRestaurant = Restaurants.FindAll(r => r.Cuisine.Id == id);
            return foundRestaurant;
        }

        private IEnumerable<Review> FakeGetReviewsForRestaurant(int id)
        {
            var foundRestaurant = Restaurants.Find(r => r.Id == id);
            return foundRestaurant?.Reviews;
        }

        private IEnumerable<Restaurant> FakeGetAllRestaurants()
        {
            foreach (var rest in Restaurants)
                yield return rest;
        }

        private void FakeUpdateRestaurant(UpdateRestaurantRequestModel updateRestaurantRequestModel)
        {
            var cuisineRef = Cuisines[updateRestaurantRequestModel.CuisineId - 1];
            var findRestToUpdate = Restaurants.Find(r => r.Id == updateRestaurantRequestModel.RestaurantId);
            if (findRestToUpdate != null)
            {
                findRestToUpdate.Cuisine = cuisineRef;
                findRestToUpdate.Name = updateRestaurantRequestModel.Name;
                findRestToUpdate.UpdatedBy = updateRestaurantRequestModel.UserId;
            }
        }

        private Restaurant FakeGetRestaurantWithReviewesById(int restaurantId)
        {
            return FakeGetRestaurantById(restaurantId);
        }


        private int FakeAddReview(AddReviewRequestModel addReviewRequestModel)
        {
            //var reviewedRestaurant = Repo.GetRestaurantById(addReviewRequestModel.RestaruntId);
            var reviewedUser = Repo.GetUserById(addReviewRequestModel.UserId);
            var reviewToAdd = new Review
            {
                Comment = addReviewRequestModel.Comment,
                CreatedBy = addReviewRequestModel.UserId,
                PostedDateTime = addReviewRequestModel.DateTimePosted,
                Rating = addReviewRequestModel.Rating,
                UpdatedBy = addReviewRequestModel.UserId,
                //ReviewRestaurant = reviewedRestaurant,
                ReviewUser = reviewedUser,
                ReviewNumber = Reviews.Count+1
               };
            Reviews.Add(reviewToAdd);

            return Reviews.Count;
        }

        private Restaurant FakeGetRestaurantById(int id)
        {
            var foundRestaurant = Restaurants.Find(r => r.Id == id);
            return foundRestaurant;
        }

        private void FakeRemoveRestaurant(RemoveRestaurantRequestModel r)
        {
            for(var i=0; i<Restaurants.Count; i++)
            {
                if (Restaurants[i].Id == r.RestaurantId) Restaurants.RemoveAt(i);
            }
        }

        private bool FakeResturantExisits(string restNameToCheck)
        {
            return Restaurants.Any(r => r.Name.Equals(restNameToCheck.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        private bool FakeResturantIdExisits(int restId)
        {
            return Restaurants.Any(r => r.Id == restId);
        }

        private int FakeAddRestaurant(AddRestaurantRequestModel r)
        {
            var cuisineRef = Cuisines[r.CuisineId - 1];
            Restaurants.Add(new Restaurant
            {
                Id = Restaurants.Count + 1,
                Cuisine = cuisineRef,
                CreatedBy = r.UserId,
                UpdatedBy = r.UserId,
                Name = r.Name
            });
            return Restaurants.Count;
        }
   }
}