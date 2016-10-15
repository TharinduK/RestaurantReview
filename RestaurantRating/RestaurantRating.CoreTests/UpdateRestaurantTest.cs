using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class UpdateRestaurantTest : MockTestSetup
    {
        [TestMethod]
        public void UpdateRestaurant_ValidIDUpdateName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Name";
            var expectedCuisine = "Cuisine2";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                Cuisine = expectedCuisine
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        private void ValidateRestUpdate(int expectedId, string expectedName, string expectedCuisine, int expectedCreatedById, int expectedUpdatedById, UpdateRestaurantRequestModel restToUpdate)
        {
            var actualRest = Restaurants.Find(r => r.Id == restToUpdate.RestaurantId);
            Assert.IsNotNull(actualRest, "Update restaurant not found");
            Assert.AreEqual(expectedId, actualRest.Id, "Restaurant ID");
            Assert.AreEqual(expectedName, actualRest.Name, "Restaurant Name");
            Assert.AreEqual(expectedCuisine, actualRest.Cuisine, "Restaurant Cuisine");
            Assert.AreEqual(expectedCreatedById, actualRest.CreatedBy, "Restaurant Created By");
            Assert.AreEqual(expectedUpdatedById, actualRest.UpdatedBy, "Restaurant Updated by");
        }

        [TestMethod]
        public void UpdateRestaurant_ValidIDUpdateCuisine_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = "New Cuisine";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                Cuisine = expectedCuisine
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void UpdateRestaurant_ValidIDNoDataToUpdate_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = "Cuisine2";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 102;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                Cuisine = expectedCuisine
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void UpdateRestaurant_ValidIDWithBlankCuisineUpdateName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var expectedCuisine = "Cuisine2";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void UpdateRestaurant_ValidIDWithBlankUpdateCuisine_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = "New Cuisine";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Cuisine = expectedCuisine
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }
        [TestMethod]
        public void UpdateRestaurant_ValidIDWithBlankUpdatePaddedCuisine_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = "New Cuisine";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Cuisine = "   " + expectedCuisine + "  "
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }
        [TestMethod]
        public void UpdateRestaurant_ValidIDWithBlankUpdatePaddedName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var expectedCuisine = "Cuisine2";
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = "  " + expectedName + "   "
            };
            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }
        [TestMethod]
        public void UpdateRestaurant_NonExistingID_Fail()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = "Cuisine1", Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = "Cuisine2", Name = "Restaurant Two" });
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = 200,
                Name = "New Name"
            };
            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = false };

            var updateRestTran = new UpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");
        }

        [TestMethod]
        [Ignore]
        public void UpdateRestaurant_WithExistingReviews_Succeed()
        {
            Assert.Fail();
        }
    }
}
