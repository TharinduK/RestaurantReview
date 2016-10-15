using System;
using System.Runtime.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class AddReviewTests :MockTestSetup
    {
        [TestMethod]
        [Ignore]
        public void AddReview_validReviewRatingThree_succeed()
        {
            //Assign 
            var expectedRating = 3;
            var expectedPostedDateTime = DateTime.Now;
            var expectedComment = "rating comment 5";
            var expectedRestId = 1;
            var expectedCreatedById = 100;
            var expectedUpdatedById = expectedCreatedById;
            var expectedReviewNumber = 5;
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

            var actualReview = Reviews[expectedResponse.ReviewNumber-1];
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
        [Ignore]
        public void AddReview_validReviewRatingOne_succeed()
        {
            //succeed 
            Assert.Fail();
        }
        [TestMethod]
        [Ignore]
        public void AddReview_vlaidReviewRatingFive_succeed()
        {
            //succeed 
            Assert.Fail();
        }
        [TestMethod]
        [Ignore]
        public void AddReview_InvlaidUserId_Fail()
        {
            //succeed 
            Assert.Fail();
        }
        [TestMethod]
        [Ignore]
        public void AddReview_InvlaidRestId_Fail()
        {
            //succeed 
            Assert.Fail();
        }
        [TestMethod]
        [Ignore]
        public void AddReview_RatingTooLow_Fail()
        {
            //succeed 
            Assert.Fail();
        }
        [TestMethod]
        [Ignore]
        public void AddReview_RaitingTooHigh_Fail()
        {
            //succeed 
            Assert.Fail();
        }
    }
}
