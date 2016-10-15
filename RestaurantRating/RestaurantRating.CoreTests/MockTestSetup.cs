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
        protected IApplicationLog Log;

        [TestInitialize]
        public void SetUp()
        {
            var log = new Mock<IApplicationLog>();
            Log = log.Object;

            var repo = new Mock<IRepository>();
            repo.Setup(m => m.AddRestaurentGetNewID(It.IsAny<AddRestaurantRequestModel>()))
                .Returns<AddRestaurantRequestModel>(r => FakeAddRestaurant(r));
                        
            repo.Setup(m => m.DoseRestaurentAlreadyExist(It.IsAny<AddRestaurantRequestModel>()))
                .Returns<AddRestaurantRequestModel>(r => FakeResturantExisits(r));

            repo.Setup(m => m.DoseRestaurentIdAlreadyExist(It.IsAny<int>()))
                .Returns<int>(id => FakeResturantIdExisits(id));

            repo.Setup(m => m.RemoveRestaurentID(It.IsAny<RemoveRestaurantRequestModel>()))
                .Callback<RemoveRestaurantRequestModel>(r => FakeRemoveRestaurant(r));

            repo.Setup(m => m.GetRestaurantByID(It.IsAny<int>()))
                .Returns<int>(id => FakeGetRestaurantById(id));

            repo.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Returns<AddReviewRequestModel>(r => FakeAddReview(r));

            repo.Setup(m => m.GetUserById(It.IsAny<int>()))
                .Returns<int>(id => Users[id - 1]);

            repo.Setup(m => m.DoseUserIdAlreadyExist(It.IsAny<int>()))
                .Returns<int>(id => id <= Users.Count && id > 0);
            Repo = repo.Object;
        }

        

        private int FakeAddReview(AddReviewRequestModel addReviewRM)
        {
            var reviewedRestaurant = Repo.GetRestaurantByID(addReviewRM.RestaruntId);
            var reviewedUser = Repo.GetUserById(addReviewRM.UserId);
            var reviewToAdd = new Review
            {
                Comment = addReviewRM.Comment,
                CreatedBy = addReviewRM.UserId,
                PostedDateTime = addReviewRM.DateTimePosted,
                Rating = addReviewRM.Rating,
                UpdatedBy = addReviewRM.UserId,
                ReviewRestaurant = reviewedRestaurant,
                ReviewUser = reviewedUser,
                ReviewNumber = Reviews.Count+1
               };
            Reviews.Add(reviewToAdd);

            return Reviews.Count;
        }

        private Restaurant FakeGetRestaurantById(int id)
        {
            return Restaurants.Find(r => r.Id == id);
        }

        private void FakeRemoveRestaurant(RemoveRestaurantRequestModel r)
        {
            for(var i=0; i<Restaurants.Count; i++)
            {
                if (Restaurants[i].Id == r.RestaurantId) Restaurants.RemoveAt(i);
            }
        }

        private bool FakeResturantExisits(AddRestaurantRequestModel restToAdd)
        {
            return Restaurants.Any(r => r.Name.Equals(restToAdd.Name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        private bool FakeResturantIdExisits(int restId)
        {
            return Restaurants.Any(r => r.Id == restId);
        }

        private int FakeAddRestaurant(AddRestaurantRequestModel r)
        {
            Restaurants.Add(new Restaurant
            {
                Id = Restaurants.Count + 1,
                Cuisine = r.Cuisine,
                CreatedBy = r.UserId,
                UpdatedBy = r.UserId,
                Name = r.Name
            });
            return Restaurants.Count;
        }
   }
}