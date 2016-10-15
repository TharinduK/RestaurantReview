using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass()]
    public class AddRestaurentTests : MockTestSetup
    {

        //unit of work_state under test_expected behavior
        [TestMethod()]
        public void AddResturant_NewResturant_AddSucessfully()
        {
            //Assign 
            var expectedName = "Restaurant name one";
            var expectedCuisine = "Cuisine 1";
            var expectedCreatedById = 1;
            var expectedUpdatedById = expectedCreatedById;
            var expectedRestID = 1;
            var reqData = new AddRestaurantRequestModel()
            {
                Name = expectedName,
                Cuisine = expectedCuisine,
                UserId = expectedCreatedById
            };
            var expectedSucessStatus = true;
            var expectedResponse = new AddRestaurantResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                RestaurantId = expectedRestID
            };
            
            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            //act
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            Restaurant actualRest = Restaurants[expectedResponse.RestaurantId - 1];
            Assert.AreEqual(expectedRestID, actualRest.Id, "Restaurant ID");
            Assert.AreEqual(expectedName, actualRest.Name, "Restaurant name");
            Assert.AreEqual(expectedCuisine, actualRest.Cuisine, "Restaurant Cuisine");
            Assert.AreEqual(expectedCreatedById, actualRest.CreatedBy, "Created by");
            Assert.AreEqual(expectedUpdatedById, actualRest.UpdatedBy, "Updated by");
        }

        //TODO: move to presistance validation 
        [TestMethod]
        public void AddResturant_WithExistingNameSame_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant name one", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "Restaurant name one",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);
            
            sut.Execute();

            ExistingResturantAssert(sut);
        }

        private void ExistingResturantAssert(AddRestaurentTransaction sut)
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
        public void AddResturant_WithExistingNameUpperCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "RESTAURANT 1",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        public void AddResturant_WithExistingNameLowerCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "restaurant 1",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        public void AddResturant_WithExistingNameMixCase_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "ResTAurant 1",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        public void AddResturant_WithExistingNameExtratSpace_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "   Restaurant 1  ",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
        [TestMethod]
        [Ignore]
        public void AddResturant_WithExistingNameExtratSpaceInMiddle_ResturantExistExceptionThrown()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant 1", CreatedBy = 100, Cuisine = "Cuisine 1", Id = 1 });
            var reqData = new AddRestaurantRequestModel()
            {
                Name = "   Restaurant    1  ",
                Cuisine = "Cuisine 2",
                UserId = 1
            };

            var sut = new AddRestaurentTransaction(Repo, Log, reqData);

            sut.Execute();

            ExistingResturantAssert(sut);
        }
    }
}