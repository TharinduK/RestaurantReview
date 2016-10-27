using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class RemoveRestaurantTest : MockTestSetup
    {
        public RemoveRestaurantTest()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1 });
        }
        [TestMethod]
        public void RemoveRestaurant_ValidID_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101,Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 101, Cuisine = Cuisines[1], Name = "Restaurant Two" });

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
            foreach (var rest in Restaurants)
            {
                if (rest.Id == restaruntToRemove.RestaurantId) return true;
            }
            return false;
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantNotFoundException))]
        public void RemoveRestaurant_NonExistingID_Fail()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101,Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 101, Cuisine = Cuisines[1], Name = "Restaurant Two" });

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
    }
}
