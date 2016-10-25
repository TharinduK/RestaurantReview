using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using RestaurantRating.Domain;

namespace RestaurantRating.APITests
{
    [TestClass()]
    public class CuisineControllerTests : ControllerTestsBase
    {
        #region Get All Cuisines
        [TestMethod]
        public void GetAllCuisines_OneCuisine_OK()
        {
            //arrange
            var cuisineName = "Mexican";
            var cuisineId = 1;
            var createdUser = 10;

            var cuisine = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            var expectedResponse = new List<API.ViewModels.Cuisine>()
            {
                new API.ViewModels.Cuisine{Id = cuisineId,Name = cuisineName}
            };
            var expectedCuisineCount = 1;
            var repoResonse = new[] { cuisine };
            MockRepository.Setup(m => m.GetAllCuisines())
                .Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                MockLogger.Object);//,
                //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Cuisine>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateCuisineCollectionResponse(contentResult, expectedResponse, expectedCuisineCount);
        }

        [TestMethod]
        public void GetAllCuisines_twoCuisine_OK()
        {
            //arrange
            var cuisineName = "Mexican";
            var cuisineId = 1;
            var createdUser = 10;
            var cuisineName2 = "Indian";
            var cuisineId2 = 2;

            var cuisine1 = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            var cuisine2 = new Cuisine
            {
                Name = cuisineName2,
                Id = cuisineId2,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            var expectedResponse = new List<API.ViewModels.Cuisine>()
            {
                new API.ViewModels.Cuisine{Id = cuisineId,Name = cuisineName},
                new API.ViewModels.Cuisine{Id = cuisineId2,Name = cuisineName2}
            };
            var expectedCuisineCount = 2;

            var repoResonse = new[] { cuisine1, cuisine2 };
            MockRepository.Setup(m => m.GetAllCuisines()).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                MockLogger.Object);//',
                //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Cuisine>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateCuisineCollectionResponse(contentResult, expectedResponse, expectedCuisineCount);
        }

        private void ValidateCuisineCollectionResponse(OkNegotiatedContentResult<IEnumerable<API.ViewModels.Cuisine>> contentResult, List<API.ViewModels.Cuisine> expectedResponse, int expectedCount)
        {
            var cuisineCountIndex = 0;
            foreach (var actualCuisine in contentResult.Content)
            {
                var expectedCuisine = expectedResponse[cuisineCountIndex];
                ValidateCuisineResponse(expectedCuisine, actualCuisine);
                cuisineCountIndex++;
            }
            Assert.AreEqual(expectedCount, cuisineCountIndex);
        }

        private void ValidateCuisineResponse(API.ViewModels.Cuisine expectedCuisine, API.ViewModels.Cuisine actualCuisine)
        {
            Assert.AreEqual(expectedCuisine.Id, actualCuisine.Id);
            Assert.AreEqual(expectedCuisine.Name, actualCuisine.Name);
        }

        [TestMethod]
        public void GetAllCuisines_NoCuisine_Empty()
        {
            //arrange
            var expectedResponse = new List<API.ViewModels.Cuisine>();
            var expectedCuisineCount = 0;

            var repoResonse = Enumerable.Empty<Domain.Cuisine>();
            MockRepository.Setup(m => m.GetAllCuisines()).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                MockLogger.Object);//',
                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Cuisine>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateCuisineCollectionResponse(contentResult, expectedResponse, expectedCuisineCount);
        }

        [TestMethod()]
        public void GetAllCuisines_DatabaseException_BadData()
        {
            //arrange
            MockRepository.Setup(m => m.GetAllCuisines())
                .Throws(new Exception());

            var ctrl = new CuisinesController(MockRepository.Object,
                MockLogger.Object);//',
                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));
                                   //act
            var actionResult = ctrl.Get();

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void GetAllCuisines_ServerException_InternalError()
        {
            //var ctrl = new CuisinesController(MockRepository.Object, MockLogger.Object, null);
            var ctrl = new CuisinesController(null, MockLogger.Object);
            //act
            var actionResult = ctrl.Get();

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        #endregion

        #region Restaurant Get All
        [TestMethod]
        public void GetRestaurantsOnCuisineId_OneRestaurnat_OK()
        {
            //arrange
            var expectedRestID = 1234;
            var cuisineName = "Mexican";
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            var createdUser = 10;
            var expectedCollectionCount = 1;
            var cuisine = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            var expectedResponse = new[]
            {
                new API.ViewModels.Restaurant{
                    Id = expectedRestID,
                    AverageRating = 0,
                    CuisineName = cuisineName,
                    CuisineId = cuisineId,
                    Name = restName,
                    ReviewCount = 0
                }
            };

            var repoResonse = new[]
            {
                new Restaurant
                {
                    Id = expectedRestID,
                    CreatedBy = createdUser,
                    UpdatedBy = createdUser,
                    Cuisine = cuisine,
                    Name = restName
                }
            };

            MockRepository.Setup(m => m.DoseCuisineIdExist(cuisineId)).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantForCuisine(cuisineId)).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                MockLogger.Object);//,
                //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(cuisineId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            RestaurantsControllerTests.ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }

        [TestMethod]
        public void GetRestaurantsOnCuisineId_TwoRestaurnat_OK()
        {
            //arrange
            var restID = 1234;
            var cuisineName = "Mexican";
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            var createdUser = 10;
            var expectedCollectionCount = 2;
            var cuisine = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            var expectedResponse = new[]
            {
                new API.ViewModels.Restaurant
                {
                    Id = restID,
                    AverageRating = 0,
                    CuisineName = cuisineName,
                    CuisineId = cuisineId,
                    Name = restName,
                    ReviewCount = 0
                },
                new API.ViewModels.Restaurant
                {
                    Id = restID+1,
                    AverageRating = 0,
                    CuisineName = cuisineName,
                    CuisineId = cuisineId,
                    Name = restName + " 1",
                    ReviewCount = 0
                }
            };

            var repoResonse = new[]
            {
                    new Restaurant
                    {
                        Id = restID,
                        CreatedBy = createdUser,
                        UpdatedBy = createdUser,
                        Cuisine = cuisine,
                        Name = restName
                    },
                    new Restaurant
                    {
                        Id = restID + 1,
                        CreatedBy = createdUser,
                        UpdatedBy = createdUser,
                        Cuisine = cuisine,
                        Name = restName + " 1",
                    }
                };

            MockRepository.Setup(m => m.DoseCuisineIdExist(cuisineId)).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantForCuisine(cuisineId)).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(cuisineId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");

            RestaurantsControllerTests.ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }


        [TestMethod]
        public void GetRestaurantsOnCuisineId_NoRestaurnats_Empty()
        {
            //arrange
            var expectedResponse = new API.ViewModels.Restaurant[] { };

            var repoResonse = new Restaurant[] { };
            var cuisineId = 5;
            var expectedCollectionCount = 0;

            MockRepository.Setup(m => m.DoseCuisineIdExist(cuisineId)).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantForCuisine(cuisineId)).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(cuisineId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;


            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            RestaurantsControllerTests.ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }

        [TestMethod]
        public void GetRestaurantsOnCuisineId_InvalidCuisineID_NotFound()
        {
            //arrange
            var repoResonse = new Restaurant[] { };
            var cuisineId = 5;

            MockRepository.Setup(m => m.DoseCuisineIdExist(cuisineId)).Returns(false);
            MockRepository.Setup(m => m.GetRestaurantForCuisine(cuisineId)).Returns(repoResonse);

            var ctrl = new CuisinesController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(cuisineId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetRestaurantsOnCuisineId_DatabaseException_BadData()
        {
            //arrange
            var cuisineId = 4;
            MockRepository.Setup(m => m.DoseCuisineIdExist(cuisineId)).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantForCuisine(cuisineId)).Throws(new Exception());

            var ctrl = new CuisinesController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(cuisineId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void GetRestaurantsOnCuisineId_ServerException_InternalError()
        {
            //arrange
            var cuisineId = 4;
            //var ctrl = new CuisinesController(MockRepository.Object, MockLogger.Object, null);
            var ctrl = new CuisinesController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Get(cuisineId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        #endregion
    }
}