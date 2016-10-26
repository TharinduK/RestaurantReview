using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.API;
using Moq;
using RestaurantRating.Domain;
using System.Web.Http.Results;

namespace RestaurantRating.APITests
{
    [TestClass()]
    public class RestaurantsControllerTests : ControllerTestsBase
    {
        #region Restaurant Get All
        [TestMethod]
        public void GetAllRestaurants_OneRestaurnat_OK()
        {
            //arrange
            var restID = 1234;
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
                    Id = restID,
                    AverageRating = 0,
                    CuisineName = cuisineName,
                    CuisineId = cuisineId,
                    Name = restName,
                    ReviewCount = 0
                }
            };

            var repoResonse = new[] {
            new Restaurant{
                Id = restID,
                CreatedBy = createdUser,
                UpdatedBy = createdUser,
                Cuisine = cuisine,
                Name = restName
            }
            };

            MockRepository.Setup(m => m.GetAllRestaurantsWithReview())
                .Returns(repoResonse);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }

        [TestMethod]
        public void GetAllRestaurants_TwoRestaurnat_OK()
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

            MockRepository.Setup(m => m.GetAllRestaurantsWithReview())
                .Returns(repoResonse);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");

            ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }

        internal static void ValidateRestaurantCollectionResponse(IEnumerable<API.ViewModels.Restaurant> responseRestaurants, API.ViewModels.Restaurant[] expectedResponse, int expectedCollectionCount)
        {
            var restaruantCountIndex = 0;
            foreach (var actualRestaurantViewMode in responseRestaurants)
            {
                var expectedRest = expectedResponse[restaruantCountIndex];
                ValidateRestaurantResponse(expectedRest, actualRestaurantViewMode);
                restaruantCountIndex++;
            }
            Assert.AreEqual(expectedCollectionCount, restaruantCountIndex);
        }

        private static void ValidateRestaurantResponse(API.ViewModels.Restaurant expectedRest, API.ViewModels.Restaurant actualRestaurantViewMode)
        {
            Assert.AreEqual(expectedRest.Id, actualRestaurantViewMode.Id, "");
            Assert.AreEqual(expectedRest.Name, actualRestaurantViewMode.Name);
            Assert.AreEqual(expectedRest.AverageRating, actualRestaurantViewMode.AverageRating);
            Assert.AreEqual(expectedRest.CuisineId, actualRestaurantViewMode.CuisineId);
            Assert.AreEqual(expectedRest.CuisineName, actualRestaurantViewMode.CuisineName);
            Assert.AreEqual(expectedRest.ReviewCount, actualRestaurantViewMode.ReviewCount);
        }

        [TestMethod]
        public void GetAllRestaurants_NoRestaurnats_Empty()
        {
            //arrange
            var expectedResponse = new API.ViewModels.Restaurant[] { };
            var expectedCollectionCount = 0;

            var repoResonse = new Restaurant[] { };

            MockRepository.Setup(m => m.GetAllRestaurantsWithReview())
                .Returns(repoResonse);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Restaurant>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateRestaurantCollectionResponse(contentResult.Content, expectedResponse, expectedCollectionCount);
        }

        [TestMethod()]
        public void GetAllRestaurants_DatabaseException_BadData()
        {
            //arrange
            MockRepository.Setup(m => m.GetAllRestaurantsWithReview())
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get();

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void GetAllRestaurants_ServerException_InternalError()
        {
            //arrange
            var ctrl = new RestaurantsController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Get();

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        #endregion

        #region Restaurant Get ID
        [TestMethod()]
        public void RestaurantGetByID_ValidRestaurantID_OK()
        {
            //arrange
            var restID = 555;
            var cuisineName = "Mexican";
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            var createdUser = 10;
            var cuisine = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            var expectedResponse = new API.ViewModels.Restaurant
            {
                Id = restID,
                AverageRating = 3,
                CuisineName = cuisineName,
                CuisineId = cuisineId,
                Name = restName,
                ReviewCount = 100
            };

            var repoResonse = new Restaurant
            {
                Id = restID,
                CreatedBy = createdUser,
                UpdatedBy = createdUser,
                Cuisine = cuisine,
                Name = restName
            };
            //restaurant 4 setup 
            for (var i = 0; i < 100; i++)
            {
                var newReview = new Review
                {
                    CreatedBy = createdUser,
                    UpdatedBy = createdUser,
                    Comment = $"Comment {i} for Restaurant 555",
                    Rating = (i % 5) + 1,
                    PostedDateTime = new DateTime(2016, 07, 1).AddDays(i + 1),
                    ReviewNumber = 4 + i,
                    ReviewUser = new User { Id = createdUser, UserName = "Ruchira" }
                };
                // 0+1 to 4+1

                repoResonse.AddReview(newReview);
            }
            MockRepository.Setup(m => m.GetRestaurantWithReviewsById(restID))
                .Returns(repoResonse);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Get(restID);
            var contentResult = actionResult as OkNegotiatedContentResult<API.ViewModels.Restaurant>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateRestaurantResponse(expectedResponse, contentResult.Content);
            //Assert.AreEqual(expectedResponse.Id, responseRestaurants.Content.Id);
            //Assert.AreEqual(expectedResponse.Name, responseRestaurants.Content.Name);
            //Assert.AreEqual(expectedResponse.AverageRating, responseRestaurants.Content.AverageRating);
            //Assert.AreEqual(expectedResponse.Cuisine, responseRestaurants.Content.Cuisine);
            //Assert.AreEqual(expectedResponse.ReviewCount, responseRestaurants.Content.ReviewCount);
        }

        [TestMethod()]
        public void RestaurantGetByID_InvalidRestaurantID_NotFound()
        {
            //arrange
            var restID = 600;

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

                                                   //act
            var actionResult = ctrl.Get(restID);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void RestaurantGetByID_DatabaseException_BadData()
        {
            //arrange
            var restID = 555;

            MockRepository.Setup(m => m.GetRestaurantWithReviewsById(restID))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                MockLogger.Object);//,
                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));
                                   //act
            var actionResult = ctrl.Get(restID);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void RestaurantGetByID_ServerException_InternalError()
        {
            //arrange
            var restID = 555;

            MockRepository.Setup(m => m.GetRestaurantWithReviewsById(restID))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Get(restID);

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        #endregion

        #region post/create new restaurant 
        [TestMethod]
        public void PostNewRestaurant_ValidInput_OkWithUrl()
        {
            //arrange
            var cuisineName = "Mexican";
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            var expectedRestId = 155;
            var createdUser = 1;
            var cuisine = new Cuisine
            {
                Name = cuisineName,
                Id = cuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            var expectedResponse = new API.ViewModels.Restaurant
            {
                Id = expectedRestId,
                AverageRating = 0,
                CuisineName = cuisineName,
                CuisineId = cuisineId,
                Name = restName,
                ReviewCount = 0
            };

            var transactionRequest = new AddRestaurantRequestModel
            {
                CuisineId = cuisineId,
                Name = restName,
                UserId = createdUser
            };

            MockRepository.Setup(m => m.AddRestaurentGetNewId(transactionRequest))
                .Returns(expectedRestId);
            MockRepository.Setup(m => m.GetCuisineById(It.IsAny<int>())).Returns(cuisine);

            var ctrl = new RestaurantsController(MockRepository.Object, MockLogger.Object);

            //act
            var actionResult = ctrl.Post(new API.ViewModels.Restaurant { CuisineId = cuisineId, Name = restName });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<API.ViewModels.Restaurant>;

            //assert
            Assert.IsNotNull(createdResult, "created-201 status was not returned");
            Assert.AreEqual("DefaultRouting", createdResult.RouteName);
            Assert.AreEqual(expectedRestId, createdResult.RouteValues["id"]);

            //validate response
            ValidateRestaurantResponse(createdResult.Content, expectedResponse);
        }

        [TestMethod()]
        public void PostNewRestaurant_DatabaseException_BadData()
        {
            //arrange
            var cuisineName = "Mexican";
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            var createdUser = 10;
            
            MockRepository.Setup(m => m.AddRestaurentGetNewId(It.IsAny<AddRestaurantRequestModel>()))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Post(new API.ViewModels.Restaurant { CuisineId = cuisineId, Name = restName });

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PostNewRestaurant_NameAlreadyExistsException_BadData()
        {
            //arrange
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";
            
            MockRepository.Setup(m => m.AddRestaurentGetNewId(It.IsAny<AddRestaurantRequestModel>()))
                .Throws(new RestaurantAlreadyExistsException());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Post(new API.ViewModels.Restaurant { CuisineId = cuisineId, Name = restName });

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PostNewRestaurant_ServerException_InternalError()
        {
            //arrange
            var cuisineId = 10;
            var restName = "No1 Mexican Restaurant";

            var ctrl = new RestaurantsController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Post(new API.ViewModels.Restaurant { CuisineId = cuisineId, Name = restName });

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        #endregion

        #region Delete Restaurant

        [TestMethod]
        public void DeleteRestaurant_ValidId_OK()
        {
            //arrange
            var RestIdToDelete = 155;
            var expectedStatusCode = HttpStatusCode.NoContent;

            var transactionRequest = new RemoveRestaurantRequestModel
            {
                RestaurantId = RestIdToDelete,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.RemoveRestaurentId(transactionRequest));
            MockRepository.Setup(m => m.DoseRestaurentIdExist(155))
                .Returns(true);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Delete(RestIdToDelete);
            var deletedStatusResult = actionResult as StatusCodeResult;

            //assert
            Assert.IsNotNull(deletedStatusResult, "delete-204 status was not returned");
            Assert.AreEqual(expectedStatusCode, deletedStatusResult.StatusCode);
        }

        [TestMethod]
        public void DeleteRestaurant_InvalidId_NotFound()
        {
            //arrange
            var RestIdToDelete = 155;

            var transactionRequest = new RemoveRestaurantRequestModel
            {
                RestaurantId = RestIdToDelete,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.RemoveRestaurentId(transactionRequest));
            MockRepository.Setup(m => m.DoseRestaurentIdExist(155))
                .Returns(false);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Delete(RestIdToDelete);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteRestaurant_DatabaseException_BadData()
        {
            //arrange
            var RestIdToDelete = 555;

            MockRepository.Setup(m => m.DoseRestaurentIdExist(RestIdToDelete))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Delete(RestIdToDelete);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void DeleteRestaurant_ServerException_InternalError()
        {
            //arrange
            var RestIdToDelete = 555;

            MockRepository.Setup(m => m.DoseRestaurentIdExist(RestIdToDelete))
                .Returns(true);

            var ctrl = new RestaurantsController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Get(RestIdToDelete);

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        #endregion

        #region Put/Complete update Restaurant
        [TestMethod]
        public void PutRestaurant_ValidData_OK()
        {
            //arrange
            var cuisineName = "Indian";
            var originalCuisineId = 1;
            var originalRestaurantName = "No1 Indian Restaurant";
            var updatedCuisineName = "Mexican";
            var updateCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;
            var createdUser = 10;
            var originalCuisine = new Cuisine
            {
                Name = cuisineName,
                Id = originalCuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            //var expectedResponse = new API.ViewModels.Restaurant
            //{
            //    Id = restaruantIdToUpdate,
            //    AverageRating = 3.5,
            //    CuisineId = updatedCuisineName,
            //    Name = updatedRestaurantName,
            //    ReviewCount = 120
            //};

            var restaurantBeforeUpdate = new Restaurant
            {
                Id = restaruantIdToUpdate,
                CreatedBy = createdUser,
                UpdatedBy = createdUser,
                Cuisine = originalCuisine,
                Name = originalRestaurantName
            };

            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineId = updateCuisineId,
                CuisineName= updatedCuisineName,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updateCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest));
            MockRepository.Setup(m => m.DoseCuisineIdExist(It.IsAny<int>())).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate))
                .Returns(restaurantBeforeUpdate);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Put(restaruantIdToUpdate, requestModel);
            var contentResult = actionResult as OkNegotiatedContentResult<API.ViewModels.Restaurant>;

            //assert
            Assert.IsNotNull(contentResult, "ok -200 status was not returned");
            //Assert.AreEqual(HttpStatusCode..Accepted, responseRestaurants.StatusCode);  
            //TODO: must check if se should return 201-accepted, 200-ok(with body) or 204 (ok with no content)
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(restaruantIdToUpdate, contentResult.Content.Id);

            ////validate response
            //ValidateRestaurantResponse(responseRestaurants.Content, expectedResponse);
        }

        [TestMethod]
        public void PutRestaurant_InvalidId_NotFound()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;
            var createdUser = 10;
            var cuisine = new Cuisine
            {
                Name = updatedCuisineName,
                Id = updatedCuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };


            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                CuisineName = updatedCuisineName,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest));
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate));

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Put(restaruantIdToUpdate, requestModel);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void PutRestaurant_DatabaseException_BadData()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;
            var createdUser = 10;
            var cuisine = new Cuisine
            {
                Name = updatedCuisineName,
                Id = updatedCuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            
            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineName = updatedCuisineName,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest))
                .Throws(new Exception());
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Put(restaruantIdToUpdate, requestModel);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PutRestaurant_ServerException_InternalError()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;
            var createdUser = 10;
            var cuisine = new Cuisine
            {
                Name = updatedCuisineName,
                Id = updatedCuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };


            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineName = updatedCuisineName,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest))
            .Throws(new Exception());
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate));

            var ctrl = new RestaurantsController(null, MockLogger.Object);


            //act
            var actionResult = ctrl.Put(restaruantIdToUpdate, requestModel);

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        #endregion

        #region Patch/partial update Restaurant
        [TestMethod]
        public void PatchRestaurant_ValidData_OK()
        {
            //arrange
            var originalCuisineName = "Indian";
            var originalCuisineId = 1;
            var originalRestaurantName = "No1 Indian Restaurant";
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;
            var createdUser = 10;
            var originalCuisine = new Cuisine
            {
                Name = originalCuisineName,
                Id = originalCuisineId,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };

            //var expectedResponse = new API.ViewModels.Restaurant
            //{
            //    Id = restaruantIdToUpdate,
            //    AverageRating = 3.5,
            //    CuisineId = updatedCuisineName,
            //    Name = updatedRestaurantName,
            //    ReviewCount = 120
            //};

            var restaurantBeforeUpdate = new Restaurant
            {
                Id = restaruantIdToUpdate,
                CreatedBy = createdUser,
                UpdatedBy = createdUser,
                Cuisine = originalCuisine,
                Name = originalRestaurantName
            };

            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest));
            MockRepository.Setup(m => m.DoseCuisineIdExist(It.IsAny<int>())).Returns(true);
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate))
                .Returns(restaurantBeforeUpdate);

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Patch(restaruantIdToUpdate, requestModel);
            var contentResult = actionResult as OkNegotiatedContentResult<API.ViewModels.Restaurant>;

            //assert
            Assert.IsNotNull(contentResult, "ok -200 status was not returned");
            //Assert.AreEqual(HttpStatusCode..Accepted, responseRestaurants.StatusCode);  
            //TODO: must check if se should return 201-accepted, 200-ok(with body) or 204 (ok with no content)
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(restaruantIdToUpdate, contentResult.Content.Id);

            ////validate response
            //ValidateRestaurantResponse(responseRestaurants.Content, expectedResponse);
        }

        [TestMethod]
        public void PatchRestaurant_InvalidId_NotFound()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;

            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineName = updatedCuisineName,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest));
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate));

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Patch(restaruantIdToUpdate, requestModel);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void PatchRestaurant_DatabaseException_BadData()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;

            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineName = updatedCuisineName,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };


            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest))
                .Throws(new Exception());
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate))
                .Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object,
                                MockLogger.Object);//,
                                                   //new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

            //act
            var actionResult = ctrl.Patch(restaruantIdToUpdate, requestModel);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void PatchRestaurant_ServerException_InternalError()
        {
            //arrange
            var updatedCuisineName = "Mexican";
            var updatedCuisineId = 10;
            var updatedRestaurantName = "World Cafe";
            var restaruantIdToUpdate = 155;

            var requestModel = new API.ViewModels.Restaurant
            {
                Id = restaruantIdToUpdate,
                CuisineName = updatedCuisineName,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
            };

            var transactionRequest = new UpdateRestaurantRequestModel
            {
                RestaurantId = restaruantIdToUpdate,
                CuisineId = updatedCuisineId,
                Name = updatedRestaurantName,
                UserId = CallingUserId
            };

            MockRepository.Setup(m => m.UpdateRestaurant(transactionRequest))
            .Throws(new Exception());
            MockRepository.Setup(m => m.GetRestaurantById(restaruantIdToUpdate));

            var ctrl = new RestaurantsController(null, MockLogger.Object);

            //act
            var actionResult = ctrl.Patch(restaruantIdToUpdate, requestModel);

            // assert
            //Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        #endregion
    }
}