using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class ViewRestaurantTest
    {
        [TestMethod]
        public void ViewRestaurant_ValidIDNoReviews_Succeed()
        {
            Assert.Fail();
        }

        [TestMethod]
        [Ignore]
        public void ViewRestaurant_ValidIDOneReviews_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurant_ValidIDTwoReviews_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurant_ValidIdTenReviews_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdMultiReviews_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdMultiReviewsDuplicateUser_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdReviewCount_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdReviewCountDuplicateUser_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurant_InvalidID_Fail()
        {
        }
    }
}
