﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.API.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Moq;
using RestaurantRating.API;
using RestaurantRating.Domain;
using Review = RestaurantRating.API.ViewModels.Review;

namespace RestaurantRating.APITests
{
    [TestClass()]
    public class ReviewsControllerTests : ControllerTestsBase
    {
        #region Get All reviews for Restaurant
        [TestMethod]
        public void GetAllReviewsForRestaurant_OneReview_OK()
        {
            //arrange
            var restaurantId = 1;
            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 3;
            int reviewNumber = 1;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };
            var reviewEntry = new Domain.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                ReviewNumber = reviewNumber,
                ReviewUser = postingUser,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            var repoResonse = new[] { reviewEntry };

            var expectedResponse = new List<API.ViewModels.Review>()
            {
                new API.ViewModels.Review{Comment = postedComment, PostedDateTime= postedDate, Rating= postedRating, UserName= postingUser.UserName,
                    ReviewNumber=reviewNumber}
            };
            var expectedCuisineCount = 1;

            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantId))
                .Returns(true);
            MockRepository.Setup(m => m.GetReviewsForRestaurant(restaurantId))
                .Returns(repoResonse);

            ExecuteAndValidateExpectedOkResponses(restaurantId, expectedResponse, expectedCuisineCount);
        }

        private void ValidateReviewCollectionResponse(IEnumerable<Review> actualReviewGroup, List<Review> expectedReviewGroup, int expectedCount)
        {
            var reviewCountIndex = 0;
            foreach (var actualReview in actualReviewGroup)
            {
                var expectedReview = expectedReviewGroup[reviewCountIndex];

                ValidateReviewResponse(expectedReview, actualReview);

                reviewCountIndex++;
            }
            Assert.AreEqual(expectedCount, reviewCountIndex, "Count");
        }

        private static void ValidateReviewResponse(Review expectedReview, Review actualReview)
        {
            Assert.AreEqual(expectedReview.Comment, actualReview.Comment, "Comment");
            Assert.AreEqual(expectedReview.PostedDateTime, actualReview.PostedDateTime, "Posted Time");
            Assert.AreEqual(expectedReview.Rating, actualReview.Rating, "Rating");
            Assert.AreEqual(expectedReview.ReviewNumber, actualReview.ReviewNumber, "Review Number");
            Assert.AreEqual(expectedReview.UserName, actualReview.UserName, "User Name");
        }

        [TestMethod]
        public void GetAllReviewsForRestaurant_twoReview_OK()
        {
            //arrange
            var restaurantId = 1;
            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 3;
            int reviewNumber = 1;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };
            var reviewEntry = new Domain.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                ReviewNumber = reviewNumber,
                ReviewUser = postingUser,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            var reviewEntry2 = new Domain.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate.AddDays(1),
                Rating = postedRating + 1,
                ReviewNumber = reviewNumber + 1,
                ReviewUser = postingUser,
                CreatedBy = createdUser,
                UpdatedBy = createdUser
            };
            var repoResonse = new[] { reviewEntry, reviewEntry2 };

            var expectedResponse = new List<API.ViewModels.Review>()
            {
                new API.ViewModels.Review{Comment = postedComment, PostedDateTime= postedDate, Rating= postedRating, UserName= postingUser.UserName,
                    ReviewNumber=reviewNumber},
                new API.ViewModels.Review{Comment = postedComment, PostedDateTime= postedDate.AddDays(1), Rating= postedRating+1, UserName= postingUser.UserName,
                    ReviewNumber=reviewNumber+1}
            };
            var expectedCuisineCount = 2;

            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantId))
                .Returns(true);
            MockRepository.Setup(m => m.GetReviewsForRestaurant(restaurantId))
                .Returns(repoResonse);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            ExecuteAndValidateExpectedOkResponses(restaurantId, expectedResponse, expectedCuisineCount);
        }

        private void ExecuteAndValidateExpectedOkResponses(int restaurantId, List<Review> expectedResponse, int expectedCuisineCount)
        {
            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Get(restaurantId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<API.ViewModels.Review>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
            ValidateReviewCollectionResponse(contentResult.Content, expectedResponse, expectedCuisineCount);
        }

        [TestMethod]
        public void GetAllReviewsForRestaurant_NoReview_Empty()
        {
            //arrange
            var restaurantId = 1;
            var repoResonse = new List<Domain.Review>();
            var expectedResponse = new List<API.ViewModels.Review>();
            var expectedCuisineCount = 0;

            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantId))
                .Returns(true);
            MockRepository.Setup(m => m.GetReviewsForRestaurant(restaurantId))
                .Returns(repoResonse);

            ExecuteAndValidateExpectedOkResponses(restaurantId, expectedResponse, expectedCuisineCount);
        }

        [TestMethod]
        public void GetAllReviewsForRestaurant_InvalidRestaruntId_NotFound()
        {
            //arrange
            var restaurantId = 1;

            var repoResonse = new List<Domain.Review>();
            MockRepository.Setup(m => m.GetReviewsForRestaurant(restaurantId)).Returns(repoResonse);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Get(restaurantId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetAllReviewsForRestaurant_DatabaseException_BadData()
        {
            //arrange
            var restaurantId = 1;

            //arrange
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantId))
                .Returns(true);
            MockRepository.Setup(m => m.GetReviewsForRestaurant(restaurantId))
                .Throws(new Exception());
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Get(restaurantId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod()]
        public void GetAllReviewsForRestaurant_ServerException_InternalError()
        {
            //arrange
            var restaurantId = 1;
            MockIdentity.Setup(m => m.GetRequestingUserId()).Throws(new Exception());

            var ctrl = new RestaurantsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Get(restaurantId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));

        }

        #endregion

        #region Post/create new Review 
        [TestMethod]
        public void PostNewReviewForeRestaurant_ValidInput_OkWithUrl()
        {
            //arrange
            var restaurantID = 10;

            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 1;
            int postedRating = 3;
            int expectedReviewNumber = 1;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };

            var expectedResponse = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                ReviewNumber = expectedReviewNumber,
                UserName = postingUser.UserName,
            };

            var requestReview = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                UserName = postingUser.UserName,
                //no review Number 
            };

            var transactionRequest = new AddReviewRequestModel
            {
                Comment = postedComment,
                DateTimePosted = postedDate,
                Rating = postedRating,
                RestaurantId = restaurantID,
                UserId = createdUser
            };

            MockRepository.Setup(m => m.AddReviewGetNewId(transactionRequest)).Returns(expectedReviewNumber);
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantID)).Returns(true);
            MockRepository.Setup(m => m.DoseUserIdAlreadyExist(postingUser.Id)).Returns(true);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(createdUser);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Post(restaurantID, requestReview);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<API.ViewModels.Review>;

            //assert
            Assert.IsNotNull(createdResult, "created-201 status was not returned");
            Assert.AreEqual("NewReviewForRestaurant", createdResult.RouteName);
            Assert.AreEqual(restaurantID, createdResult.RouteValues["id"]);

            //validate response
            ValidateReviewResponse(createdResult.Content, expectedResponse);
        }

        [TestMethod]
        public void PostNewReviewForeRestaurant_InvalidRestaurantID_NotFound()
        {
            //arrange
            var restaurantID = 10;
            var newReviewNumber = 12;
            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 10;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };

            var requestReview = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                UserName = postingUser.UserName,
                //no review Number 
            };

            MockRepository.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Returns(newReviewNumber);
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantID)).Returns(false);
            MockRepository.Setup(m => m.DoseUserIdAlreadyExist(postingUser.Id)).Returns(true);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Post(restaurantID, requestReview);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostNewReviewForeRestaurant_InvalidUserD_BadData()
        {
            //arrange
            var restaurantID = 10;
            var newReviewNumber = 12;
            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 10;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };

            var requestReview = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                UserName = postingUser.UserName,
                //no review Number 
            };

            MockRepository.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Returns(newReviewNumber);
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantID)).Returns(true);
            MockRepository.Setup(m => m.DoseUserIdAlreadyExist(postingUser.Id)).Returns(false);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Post(restaurantID, requestReview);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void PostNewReviewForeRestaurant_DatabaseException_BadData()
        {
            //arrange
            var restaurantID = 10;

            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 10;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };

            var requestReview = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                UserName = postingUser.UserName,
                //no review Number 
            };

            MockRepository.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Throws(new Exception());
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantID)).Returns(true);
            MockRepository.Setup(m => m.DoseUserIdAlreadyExist(postingUser.Id)).Returns(true);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Post(restaurantID, requestReview);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }


        [TestMethod()]
        public void PostNewReviewForeRestaurant_ServerException_InternalError()
        {
            //arrange
            var restaurantID = 10;

            var postedDate = new DateTime(2016, 10, 1);
            var createdUser = 10;
            int postedRating = 10;
            string postedComment = "Review comment 1";
            User postingUser = new User { Id = createdUser, UserName = "Ruchira" };

            var requestReview = new API.ViewModels.Review
            {
                Comment = postedComment,
                PostedDateTime = postedDate,
                Rating = postedRating,
                UserName = postingUser.UserName,
                //no review Number 
            };

            MockRepository.Setup(m => m.AddReviewGetNewId(It.IsAny<AddReviewRequestModel>()))
                .Throws(new Exception());
            MockRepository.Setup(m => m.DoseRestaurentIdExist(restaurantID)).Returns(true);
            MockRepository.Setup(m => m.DoseUserIdAlreadyExist(postingUser.Id)).Returns(true);
            MockIdentity.Setup(m => m.GetRequestingUserId()).Throws(new Exception());

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, MockIdentity.Object);

            //act
            var actionResult = ctrl.Post(restaurantID, requestReview);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }
        #endregion
    }
}