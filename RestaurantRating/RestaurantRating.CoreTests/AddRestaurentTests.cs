using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass()]
    public class AddRestaurentTests : MockTestSetup
    {
        public AddRestaurentTests()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 2, Name = "American", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1 });
        }

        //unit of work_state under test_expected behavior
        [TestMethod()]
        public void AddResturant_NewResturant_AddSucessfully()
        {
            //Assign 
            var expectedName = "Restaurant name one";
            var expectedCuisine = Cuisines[0].Id;
            var expectedCreatedById = 1;
            var expectedUpdatedById = expectedCreatedById;
            var expectedRestID = 1;
            var reqData = new AddRestaurantRequestModel()
            {
                Name = expectedName,
                CuisineId = expectedCuisine,
                UserId = expectedCreatedById
            };
            var expectedSucessStatus = true;
            var expectedResponse = new AddRestaurantResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                RestaurantId = expectedRestID
            };
            
            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            //act
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            Restaurant actualRest = Restaurants[expectedResponse.RestaurantId - 1];
            Assert.AreEqual(expectedRestID, actualRest.Id, "Restaurant ID");
            Assert.AreEqual(expectedName, actualRest.Name, "Restaurant name");
            Assert.AreEqual(expectedCuisine, actualRest.Cuisine.Id, "Restaurant CuisineId");
            Assert.AreEqual(expectedCreatedById, actualRest.CreatedBy, "Created by");
            Assert.AreEqual(expectedUpdatedById, actualRest.UpdatedBy, "Updated by");
        }

        //TODO: move to presistance validation 
        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void AddResturant_WithExistingNameSame_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant name one", CreatedBy = 100, UpdatedBy = 100, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "Restaurant name one",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);
            
            sut.Execute();

            ExistingResturantAssert(sut);
        }

        private void ExistingResturantAssert(AddRestaurantTransaction sut)
        {
            var expectedResturantCount = 1;
            var expectedSucessStatus = false;
            var expectedResponse = new AddRestaurantResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                RestaurantId = 0
            };

            var actualResponse = sut.Response;
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            Assert.AreEqual(expectedResturantCount, Restaurants.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void AddResturant_WithExistingNameUpperCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, UpdatedBy = 100, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "RESTAURANT 1",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void AddResturant_WithExistingNameLowerCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, UpdatedBy = 101, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "restaurant 1",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void AddResturant_WithExistingNameMixCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, UpdatedBy = 101, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "ResTAurant 1",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        [ExpectedException(typeof(RestaurantAlreadyExistsException))]
        public void AddResturant_WithExistingNameExtratSpace_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, UpdatedBy = 101, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "   Restaurant 1  ",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        [Ignore]
        public void AddResturant_WithExistingNameExtratSpaceInMiddle_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, UpdatedBy = 101, Cuisine = Cuisines[0], Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "   Restaurant    1  ",
                CuisineId = Cuisines[1].Id,
                UserId = 1
            };

            var sut = new AddRestaurantTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
    }
}