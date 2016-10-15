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
        protected List<Restaurant> Resturants = new List<Restaurant>();
        protected List<Review> Reviews = new EditableList<Review>();
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

            Repo = repo.Object;
        }

        private Restaurant FakeGetRestaurantById(int id)
        {
            return Resturants.Find(r => r.Id == id);
        }

        private void FakeRemoveRestaurant(RemoveRestaurantRequestModel r)
        {
            for(var i=0; i<Resturants.Count; i++)
            {
                if (Resturants[i].Id == r.RestaurantId) Resturants.RemoveAt(i);
            }
        }

        private bool FakeResturantExisits(AddRestaurantRequestModel restToAdd)
        {
            return Resturants.Any(r => r.Name.Equals(restToAdd.Name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        private bool FakeResturantIdExisits(int restId)
        {
            return Resturants.Any(r => r.Id == restId);
        }

        private int FakeAddRestaurant(AddRestaurantRequestModel r)
        {
            Resturants.Add(new Restaurant
            {
                Id = Resturants.Count + 1,
                Cuisine = r.Cuisine,
                CreatedBy = r.UserId,
                UpdatedBy = r.UserId,
                Name = r.Name
            });
            return Resturants.Count;
        }
   }
}