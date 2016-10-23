using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using RestaurantRating.API.Factories;
using RestaurantRating.Domain;
using Review = RestaurantRating.API.ViewModels.Review;

namespace RestaurantRating.APITests
{
    [TestClass()]
    public class ReviewsControllerTests: ControllerTestsBase
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
            User postingUser = new User {Id = createdUser, UserName = "Ruchira"};
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
                ReviewNumber = reviewNumber +1,
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

            ExecuteAndValidateExpectedOkResponses(restaurantId, expectedResponse, expectedCuisineCount);
        }

        private void ExecuteAndValidateExpectedOkResponses(int restaurantId, List<Review> expectedResponse, int expectedCuisineCount)
        {
            var ctrl = new ReviewsController(MockRepository.Object,
                MockLogger.Object,
                new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

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

            var ctrl = new ReviewsController(MockRepository.Object,
                MockLogger.Object,
                new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

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

            var ctrl = new ReviewsController(MockRepository.Object,
                MockLogger.Object,
                new TransactionFactory(MockRepository.Object, MockLogger.Object, CallingUserId));

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

            var ctrl = new ReviewsController(MockRepository.Object, MockLogger.Object, null);

            //act
            var actionResult = ctrl.Get(restaurantId);

            // assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        #endregion
    }
}