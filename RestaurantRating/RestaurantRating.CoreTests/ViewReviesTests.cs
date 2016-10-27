using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;
using System.Collections.Generic;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class ViewReviesTests :MockTestSetup
    {
        public ViewReviesTests()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1 });
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1 });

            Restaurants.Add(new Restaurant { Name = "Restaurant name one", CreatedBy = 1, UpdatedBy = 1, Cuisine = Cuisines[0], Id = 1 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Two", CreatedBy = 1, UpdatedBy = 1, Cuisine = Cuisines[0], Id = 2 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Three", CreatedBy = 1, UpdatedBy = 1, Cuisine = Cuisines[1], Id = 3 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Four", CreatedBy = 2, UpdatedBy = 2, Cuisine = Cuisines[1], Id = 4 });
            Restaurants.Add(new Restaurant { Name = "Restaurant name Five", CreatedBy = 2, UpdatedBy = 1, Cuisine = Cuisines[3], Id = 5 });


            Users.Add(new User { Id = 1 });
            Users.Add(new User { Id = 2 });
            Users.Add(new User { Id = 3 });
            Users.Add(new User { Id = 4 });
            Users.Add(new User { Id = 5 });
            Users.Add(new User { Id = 6 });
            Users.Add(new User { Id = 7 });
            Users.Add(new User { Id = 8 });
            Users.Add(new User { Id = 9 });
            Users.Add(new User { Id = 10 });
            Users.Add(new User { Id = 11 });

            //restaurant 2 setup 
            Reviews.Add(new Review
            {
                CreatedBy = 3,
                ReviewUser = Users[2],
                UpdatedBy = 3,
                //ReviewRestaurant = Restaurants[1],
                Comment = "Comment for 2",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 1,
            });
            Restaurants[1].AddReview(Reviews[0]);//rest ID 2

            //restaurant 3 setup 
            Reviews.Add(new Review
            {
                CreatedBy = 4,
                UpdatedBy = 4,
                Comment = "First Comment for 3",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 2,
                ReviewUser = Users[3]
            });
            Reviews.Add(new Review
            {
                CreatedBy = 3,
                UpdatedBy = 3,
                Comment = "Second Comment for 3",
                Rating = 5,
                PostedDateTime = new DateTime(2016, 10, 10),
                ReviewNumber = 3,
                ReviewUser = Users[2]
            });
            Restaurants[2].AddReview(Reviews[1]);
            Restaurants[2].AddReview(Reviews[2]);

            //restaurant 4 setup 
            for (var i = 0; i < 10; i++)
            {
                var newReview = new Review
                {
                    CreatedBy = Users[i].Id,
                    UpdatedBy = Users[i].Id,
                    Comment = $"Comment {i} for Restaurant 4",
                    Rating = (i % 5) + 1,
                    PostedDateTime = new DateTime(2016, 09, i + 1),
                    ReviewNumber = 4 + i,
                    ReviewUser = Users[i]
                };
                // 0+1 to 4+1

                Reviews.Add(newReview);

                Restaurants[3].AddReview(Reviews[3 + i]);
            }
        }

        [TestMethod]
        public void ViewReview_ValidRestIdOneReview_Succeed()
        {
            //arrange
            var viewingUserId = 1;
            var restaurantID = 2;
            var request = ViewRestaurantTests.SetupExpectedRequest(viewingUserId, restaurantID);

            var expectedReview = new Review
            {
                CreatedBy = 3,
                ReviewUser = Users[2],
                UpdatedBy = 3,
                Comment = "Comment for 2",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 1,
            };
            var expectedSucessStatus = true;
            var expectedReviewCount = 1;
            
            //act
            ViewReviewsForRestaurantTransaction tran = new ViewReviewsForRestaurantTransaction(Repo, Log, request);
            tran.Execute();

            //assert
            var actualdResponse = tran.Response;
            Assert.AreEqual(expectedSucessStatus, actualdResponse.WasSucessfull, "Invalid execution status");

            int actualCount = 0;
            Review actualReview = null;
            foreach (var rev in actualdResponse.Reviews)
            {
                actualCount++;
                actualReview = rev;
            }
            Assert.AreEqual(expectedReviewCount, actualCount, "unexpected review count");

            Assert.AreEqual(expectedReview, actualReview);
        }

        [TestMethod]
        public void ViewReview_ValidRestId2Reviews_Succeed()
        {
            var viewingUserId = 1;
            var restaurantID = 3;
            var request = ViewRestaurantTests.SetupExpectedRequest(viewingUserId, restaurantID);

            var expectedReviews = new List<Review> {
            new Review
            {
                CreatedBy = 4,
                UpdatedBy = 4,
                Comment = "First Comment for 3",
                Rating = 3,
                PostedDateTime = new DateTime(2016, 10, 16),
                ReviewNumber = 2,
                ReviewUser = Users[3]
            },
                new Review
            {
                CreatedBy = 3,
                UpdatedBy = 3,
                Comment = "Second Comment for 3",
                Rating = 5,
                PostedDateTime = new DateTime(2016, 10, 10),
                ReviewNumber = 3,
                ReviewUser = Users[2]
            }};

            var expectedSucessStatus = true;
            var expectedReviewCount = 2;

            //act
            ViewReviewsForRestaurantTransaction tran = new ViewReviewsForRestaurantTransaction(Repo, Log, request);
            tran.Execute();

            //assert
            AssertReviewsInResponse(expectedReviews, expectedSucessStatus, expectedReviewCount, tran);
        }

        private static void AssertReviewsInResponse(List<Review> expectedReviews, bool expectedSucessStatus, int expectedReviewCount, ViewReviewsForRestaurantTransaction tran)
        {
            var actualdResponse = tran.Response;
            Assert.AreEqual(expectedSucessStatus, actualdResponse.WasSucessfull, "Invalid execution status");

            int actualCount = 0;
            int index = 0;
            foreach (var actualReview in actualdResponse.Reviews)
            {
                Assert.AreEqual(expectedReviews[index], actualReview);
                actualCount++;
                index++;
            }
            Assert.AreEqual(expectedReviewCount, actualCount, "unexpected review count");
        }

        [TestMethod]
        public void ViewReview_ValidRestId10Reviews_Succeed()
        {
            var viewingUserId = 1;
            var restaurantID = 4;
            var request = ViewRestaurantTests.SetupExpectedRequest(viewingUserId, restaurantID);

            var expectedReviews = new List<Review>();
            for (var i = 0; i < 10; i++)
            {
                var newReview = new Review
                {
                    CreatedBy = Users[i].Id,
                    UpdatedBy = Users[i].Id,
                    Comment = $"Comment {i} for Restaurant 4",
                    Rating = (i % 5) + 1,
                    PostedDateTime = new DateTime(2016, 09, i + 1),
                    ReviewNumber = 4 + i,
                    ReviewUser = Users[i]
                };
                // 0+1 to 4+1

                expectedReviews.Add(newReview);
            }

            var expectedSucessStatus = true;
            var expectedReviewCount = 10;

            //act
            ViewReviewsForRestaurantTransaction tran = new ViewReviewsForRestaurantTransaction(Repo, Log, request);
            tran.Execute();

            //assert
            AssertReviewsInResponse(expectedReviews, expectedSucessStatus, expectedReviewCount, tran);
        }

        [TestMethod]
        public void ViewReview_ValidRestIdNoReviews_Succeed()
        {
            var viewingUserId = 1;
            var restaurantID = 1;
            var request = ViewRestaurantTests.SetupExpectedRequest(viewingUserId, restaurantID);

            var expectedReviews = new List<Review>();
            var expectedSucessStatus = true;
            var expectedReviewCount = 0;

            //act
            ViewReviewsForRestaurantTransaction tran = new ViewReviewsForRestaurantTransaction(Repo, Log, request);
            tran.Execute();

            //assert
            AssertReviewsInResponse(expectedReviews, expectedSucessStatus, expectedReviewCount, tran);
        }

        [TestMethod]
        [ExpectedException(typeof(RestaurantNotFoundException))]
        public void ViewReview_InvalidRestId_Fail()
        {
            var viewingUserId = 1;
            var restaurantID = 1010;
            var request = ViewRestaurantTests.SetupExpectedRequest(viewingUserId, restaurantID);

            var expectedReviews = new List<Review>();
            var expectedSucessStatus = false;
            var expectedReviewCount = 0;

            //act
            ViewReviewsForRestaurantTransaction tran = new ViewReviewsForRestaurantTransaction(Repo, Log, request);
            tran.Execute();

            //assert
            AssertReviewsInResponse(expectedReviews, expectedSucessStatus, expectedReviewCount, tran);
        }

    }
}
