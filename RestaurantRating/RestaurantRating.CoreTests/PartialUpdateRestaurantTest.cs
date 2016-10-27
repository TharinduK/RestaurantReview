using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;
using System;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class PartialUpdateRestaurantTest : MockTestSetup
    {
        public PartialUpdateRestaurantTest()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1 });
        }

        [TestMethod]
        public void PartialUpdateRestaurant_ValidIDUpdateName_Succeed()
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
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

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
        public void PartialUpdateRestaurant_ValidIDUpdateCuisine_Succeed()
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
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void PartialUpdateRestaurant_ValidIDNoDataToUpdate_Succeed()
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
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void PartialUpdateRestaurant_ValidIDWithBlankCuisineUpdateName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var expectedCuisine = Cuisines[1].Id;
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = expectedName,
            };
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void PartialUpdateRestaurant_ValidIDWithBlankNameUpdateCuisine_Succeed()
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
                CuisineId = expectedCuisine
            };
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }

        [TestMethod]
        public void PartialUpdateRestaurant_ValidIDWithBlankUpdatePaddedName_Succeed()
        {
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });
            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var expectedCuisine = Cuisines[1].Id;
            var expectedCreatedById = 102;
            var expectedUpdatedById = 103;
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = 103,
                RestaurantId = expectedID,
                Name = "  " + expectedName + "   "
            };
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");

            ValidateRestUpdate(expectedID, expectedName, expectedCuisine, expectedCreatedById, expectedUpdatedById, restToUpdate);
        }
        [TestMethod]
        [ExpectedException(typeof(RestaurantNotFoundException))]
        public void PartialUpdateRestaurant_NonExistingID_Fail()
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

            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;

            //assert
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");
        }

        [TestMethod]
        public void PartialUpdateRestaurant_WithExistingReviews_Succeed()
        {
            Users.Add(new User { Id = 1, FirstName = "Ruchira", LastName = "Kumara", UserName = "Ruch" });
            Restaurants.Add(new Restaurant { Id = 1, CreatedBy = 101, UpdatedBy = 101, Cuisine = Cuisines[0], Name = "Restaurant one" });
            Restaurants.Add(new Restaurant { Id = 2, CreatedBy = 102, UpdatedBy = 102, Cuisine = Cuisines[1], Name = "Restaurant Two" });

            Restaurants[1].AddReview(new Review
            {
                CreatedBy = 4,
                UpdatedBy = 4,
                Comment = "First Comment for 3",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 2,
                ReviewUser = Users[0]
            });
            Restaurants[1].AddReview(new Review
            {
                CreatedBy = 3,
                UpdatedBy = 3,
                Comment = "Second Comment for 3",
                Rating = 5,
                PostedDateTime = new DateTime(2016, 10, 10),
                ReviewNumber = 3,
                ReviewUser = Users[0]
            });

            var expectedID = 2;
            var expectedName = "New Restaurant Name";
            var restToUpdate = new UpdateRestaurantRequestModel
            {
                UserId = Users[0].Id,
                RestaurantId = expectedID,
                Name = expectedName
            };
            var expectedResponse = new UpdateRestaurantResponseModel { WasSucessfull = true };
            
            var updateRestTran = new PartialUpdateRestaurantTransaction(Repo, Log, restToUpdate);

            //act
            updateRestTran.Execute();
            var actualResponse = updateRestTran.Response;
            Assert.AreEqual(expectedResponse.WasSucessfull, actualResponse.WasSucessfull, "Invalid execution status");
        }
    }
}
