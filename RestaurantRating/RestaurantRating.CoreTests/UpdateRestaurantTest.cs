using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class CompleteUpdateRestaurantTest : MockTestSetup
    {
        public CompleteUpdateRestaurantTest()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1 });
        }
        [TestMethod]
        public void CompleteUpdateRestaurant_ValidIDUpdateName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Name";
            var expectedCuisine = Cuisines[1].Id;
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                CuisineId = expectedCuisine
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        private void ValidateRestUpdate(int expectedId, string expectedName, int expectedCuisine, int expectedCreatedById, int expectedUpdatedById, UpdateRestaurantRequestModel restToUpdate)
        {
            var actualRest = Restaurants.Find(r => r.Id == restToUpdate.RestaurantId);
            Assert.IsNotNull(actualRest, "Update restaurant not found");
            Assert.AreEqual(expectedId, actualRest.Id, "Restaurant ID");
            Assert.AreEqual(expectedName, actualRest.Name, "Restaurant Name");
            Assert.AreEqual(expectedCuisine, actualRest.Cuisine.Id, "Restaurant CuisineId");
            Assert.AreEqual(expectedCreatedById, actualRest.CreatedBy, "Restaurant Created By");
            Assert.AreEqual(expectedUpdatedById, actualRest.UpdatedBy, "Restaurant Updated by");
        }

        [TestMethod]
        public void CompleteUpdateRestaurant_ValidIDUpdateCuisine_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = Cuisines[2].Id;
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                CuisineId = expectedCuisine
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(CuisineNotFoundException))]
        public void CompleteUpdateRestaurant_ValidCuisineId_Fail()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = 2,
                CuisineId = 100,
                Name = "New Name"
            };

            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
        }
        [TestMethod]
        public void CompleteUpdateRestaurant_ValidIDNoDataToUpdate_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });

            var expectedID = 2;
            var expectedName = "Restaurant Two";
            var expectedCuisine = Cuisines[1].Id;
            var expectedCreatedById = 102;
            var expectedUpdatedById = 102;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
                CuisineId = expectedCuisine
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantInvalidInputException))]
        public void CompleteUpdateRestaurant_ValidIDWithBlankCuisineUpdateName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantInvalidInputException))]
        public void CompleteUpdateRestaurant_ValidIDWithBlankNameUpdateCuisine_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedCuisine = Cuisines[2].Id;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                CuisineId = expectedCuisine
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;
        }
        //[TestMethod]
        //[ExpectedException(typeof(RestaurantInvalidInputException))]
        //public void CompleteUpdateRestaurant_ValidIDWithBlankUpdatePaddedCuisine_Succeed()
        //{
        //    Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
        //    Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
        //    var expectedID = 2;
        //    var expectedCuisine = "New CuisineId";
        //    var restToUpdate = new UpdateRestaurantRequestModel
        //    {
        //        UserId = 103,
        //        RestaurantId = expectedID,
        //        Cuisine = "   " + expectedCuisine + "  "
        //    };
        //    var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

        //    var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

        //    //act
        //    updateRestTran.Execute();
        //    var actualResponse = updateRestTran.Response;
        //}
        [TestMethod]
        [ExpectedException(typeof(RestaurantInvalidInputException))]
        public void CompleteUpdateRestaurant_ValidIDWithBlankUpdatePaddedName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = "  " + expectedName + "   "
            };
            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantNotFoundException))]
        public void CompleteUpdateRestaurant_NonExistingID_Fail()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = 200,
                Name = "New Name"
            };
            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = false };

            var updateRestTran = new CompleteUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;
        }

        [TestMethod]
        [Ignore]
        public void CompleteUpdateRestaurant_WithExistingReviews_Succeed()
        {
            Assert.Fail();
        }
    }
}
