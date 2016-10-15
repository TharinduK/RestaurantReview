using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class RemoveRestaurantTest : MockTestSetup
    {
        [TestMethod]
        public void RemoveRestaurant_ValidID_Succeed()
        {
            Resturants.Add(new Restaurant { Id = 1, CreatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Resturants.Add(new Restaurant { Id = 2, CreatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });

            var restaruntToRemove = new RemoveRestaurantRequestModel { UserId = 103, RestaurantId = 2 };
            var removeRestTran = new RemoveRestaurantTransaction(Repo, Log, restaruntToRemove);

            var expectedResponse = new RemoveRestaurantResponseModel { WasSucessfull = true };

            //act
            removeRestTran.Execute();
            var actualResponse = removeRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.IsFalse(RestaurantExists(restaruntToRemove));
        }

        private bool RestaurantExists(RemoveRestaurantRequestModel restaruntToRemove)
        {
            foreach (var rest in Resturants)
            {
                if (rest.Id == restaruntToRemove.RestaurantId) return true;
            }
            return false;
        }

        [TestMethod]
        public void RemoveRestaurant_NonExistingID_Fail()
        {
            Resturants.Add(new Restaurant { Id = 1, CreatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Resturants.Add(new Restaurant { Id = 2, CreatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });

            var restaruntToRemove = new RemoveRestaurantRequestModel { UserId = 103, RestaurantId = 25 };
            var removeRestTran = new RemoveRestaurantTransaction(Repo, Log, restaruntToRemove);

            var expectedResponse = new RemoveRestaurantResponseModel { WasSucessfull = false };

            //act
            removeRestTran.Execute();
            var actualResponse = removeRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.IsFalse(RestaurantExists(restaruntToRemove), "Restaurant not found");
        }

        [TestMethod]
        [Ignore]
        public void RemoveRestaurant_WithExistingReviews_Fail()
        {
            Assert.Fail();
        }
    }

    [TestClass]
    public class RemoveRestauranteAndReviewsTeset
    {

    }
}
