using System;
using System.Runtime.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class AddReviewTests :MockTestSetup
    {
        public AddReviewTests()
        {
            Restaurants.Add(new Restaurant { Name = "Restaurant name one", CreatedBy = 1, Cuisine = "Cuisine 1", Id = 1 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Two", CreatedBy = 2, Cuisine = "Cuisine 2", Id = 2 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Three", CreatedBy = 3, Cuisine = "Cuisine 3", Id = 3 });

            Users.Add(new User { Id = 1 });
            Users.Add(new User { Id = 2 });
            Users.Add(new User { Id = 3 });
        }
        [TestMethod]
        public void AddReview_validReviewRatingThree_succeed()
        {
            //Assign 
            var expectedRating = 3;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 1";
            var expectedRestId = 1;
            var expectedCreatedById = 1;
            var expectedUpdatedById = expectedCreatedById;
            var expectedReviewNumber = 1;
            var expectedSucessStatus = true;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateResponse(expectedResponse, expectedRating, expectedPostedDateTime, expectedComment, expectedCreatedById, expectedUpdatedById, expectedReviewNumber, expectedRestId);
        }

        private void ValidateResponse(AddReviewResponseModel expectedResponse, int expectedRating, DateTime expectedPostedDateTime, string expectedComment,
            int expectedCreatedById, int expectedUpdatedById, int expectedReviewNumber, int expectedRestId)
        {
            var actualReview = Reviews[expectedResponse.ReviewNumber - 1];
            Assert.AreEqual(expectedRating, actualReview.Rating, "Rating");
            Assert.AreEqual(expectedPostedDateTime, actualReview.PostedDateTime, "Posted Time");
            Assert.AreEqual(expectedComment, actualReview.Comment, "Comment");
            Assert.AreEqual(expectedCreatedById, actualReview.CreatedBy, "Created by");
            Assert.AreEqual(expectedUpdatedById, actualReview.UpdatedBy, "Updated by");
            Assert.AreEqual(expectedReviewNumber, actualReview.ReviewNumber, "Review Number");

            Assert.AreEqual(expectedRestId, actualReview.ReviewRestaurant.Id, "Restaurant Id");
            Assert.AreEqual(expectedCreatedById, actualReview.ReviewUser.Id, "User Id");
        }

        [TestMethod]
        public void AddReview_validReviewRatingOne_succeed()
        {
            #region Add one review 
            Reviews.Add(new Review
            {
                Comment = "Comment 1",
                CreatedBy = 1,
                PostedDateTime = DateTime.Now.AddDays(-1),
                Rating = 3,
                ReviewNumber = 1,
                ReviewRestaurant = Restaurants[0],
                ReviewUser = Users[0],
                UpdatedBy = 1
            });
            #endregion

            //Assign 
            var expectedRating = 1;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 2";
            var expectedRestId = 2;
            var expectedCreatedById = 2;
            var expectedUpdatedById = expectedCreatedById;
            var expectedReviewNumber = 2;
            var expectedSucessStatus = true;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateResponse(expectedResponse, expectedRating, expectedPostedDateTime, expectedComment, expectedCreatedById, expectedUpdatedById, expectedReviewNumber, expectedRestId);
        }
        [TestMethod]
        public void AddReview_vlaidReviewRatingFive_succeed()
        {
            AddTwoReviews();

            //Assign 
            var expectedRating = 5;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 5";
            var expectedRestId = 2;
            var expectedCreatedById = 3;
            var expectedUpdatedById = expectedCreatedById;
            var expectedReviewNumber = 3;
            var expectedSucessStatus = true;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateResponse(expectedResponse, expectedRating, expectedPostedDateTime, expectedComment, expectedCreatedById, expectedUpdatedById, expectedReviewNumber, expectedRestId);
        }

        private void AddTwoReviews()
        {
            Reviews.Add(new Review
            {
                Comment = "Comment 1",
                CreatedBy = 1,
                PostedDateTime = DateTime.Now.AddDays(-1),
                Rating = 3,
                ReviewNumber = 1,
                ReviewRestaurant = Restaurants[0],
                ReviewUser = Users[0],
                UpdatedBy = 1
            });
            Reviews.Add(new Review
            {
                Comment = "Comment 2",
                CreatedBy = 2,
                PostedDateTime = DateTime.Now.AddDays(-2),
                Rating = 1,
                ReviewNumber = 2,
                ReviewRestaurant = Restaurants[1],
                ReviewUser = Users[1],
                UpdatedBy = 2
            });
        }

        [TestMethod]
        public void AddReview_InvlaidUserId_Fail()
        {
            AddTwoReviews();

            //Assign 
            var expectedRating = 4;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 4";
            var expectedRestId = 2;
            var expectedCreatedById = 101;
            var expectedReviewNumber = 0;
            var expectedSucessStatus = false;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            var noneAddreReview = Reviews.Find(r => r.ReviewNumber == expectedReviewNumber);
            Assert.IsNull(noneAddreReview);
        }

        [TestMethod]
        public void AddReview_InvlaidRestId_Fail()
        {
            AddTwoReviews();

            //Assign 
            var expectedRating = 4;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 4";
            var expectedRestId = 201;
            var expectedCreatedById = 1;
            var expectedReviewNumber = 0;
            var expectedSucessStatus = false;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            var noneAddreReview = Reviews.Find(r => r.ReviewNumber == expectedReviewNumber);
            Assert.IsNull(noneAddreReview);
        }
        [TestMethod]
        public void AddReview_RatingTooLow_Fail()
        {
            AddTwoReviews();

            //Assign 
            var expectedRating = 0;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 9";
            var expectedRestId = 2;
            var expectedCreatedById = 3;
            var expectedReviewNumber = 0;
            var expectedSucessStatus = false;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            var noneAddreReview = Reviews.Find(r => r.ReviewNumber == expectedReviewNumber);
            Assert.IsNull(noneAddreReview);
        }
        [TestMethod]
        public void AddReview_RaitingTooHigh_Fail()
        {
            AddTwoReviews();

            //Assign 
            var expectedRating = 9;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 9";
            var expectedRestId = 2;
            var expectedCreatedById = 3;
            var expectedReviewNumber = 0;
            var expectedSucessStatus = false;
            var reqData = new AddReviewRequestModel()
            {
                Rating = expectedRating,
                DateTimePosted = expectedPostedDateTime,
                Comment = expectedComment,
                UserId = expectedCreatedById,
                RestaruntId = expectedRestId
            };

            var expectedResponse = new AddReviewResponseModel()
            {
                WasSucessfull = expectedSucessStatus,
                ReviewNumber = expectedReviewNumber
            };

            //act
            var sut = new AddReviewTransaction(Repo, Log, reqData);
            sut.Execute();
            var actualResponse = sut.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            var noneAddreReview = Reviews.Find(r => r.ReviewNumber == expectedReviewNumber);
            Assert.IsNull(noneAddreReview);
        }
    }
}
