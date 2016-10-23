using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class ViewRestaurantTests : MockTestSetup
    {
        public ViewRestaurantTests()
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


            for (int i = 0; i < 5; i++)
            {
                Restaurants.Add(new Restaurant
                {
                    Name = $"Mexican Restaurant No {i}",
                    CreatedBy = 1,
                    UpdatedBy = 1,
                    Cuisine = Cuisines[4],
                    Id = (6 + i)
                });
            }

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
            Restaurants[1].AddReview(Reviews[0]);

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
        public void ViewRestaurant_ValidIDNoReviews_Succeed()
        {
            //TODO: review response to check if the values can be updated (they should not be able to)
            var viewingUserId = 1;
            var viewingRestID = 1;
            var expectedRequestModel = SetupExpectedRequest(viewingUserId, viewingRestID);

            var expectedName = "Restaurant name one";
            var expectedCuisine = Cuisines[0];
            var expectedRestId = 1;
            var expectedReviews = Enumerable.Empty<Review>();
            var expectedAverageRating = 0;
            var expectedReviewCount = 0;
            var expectedSucessStatus = true;
            var expectedResponse = SetupExpectedResponse(expectedSucessStatus, expectedName,
                expectedCuisine, expectedRestId, expectedReviews, expectedAverageRating, expectedReviewCount);


            var viewRestTran = new ViewRestaurantTransaction(Repo, Log, expectedRequestModel);

            viewRestTran.Execute();
            var actualResponse = viewRestTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateRestaurant(expectedRestId, expectedName, expectedCuisine, expectedReviews, expectedAverageRating, expectedReviewCount);
        }

        internal static ViewRestaurantRequestModel SetupExpectedRequest(int updatingUserId, int expectedRestId)
        {
            return new ViewRestaurantRequestModel
            {
                UserId = updatingUserId,
                RestaurantId = expectedRestId
            };
        }

        private static ViewRestaurantResponseModel SetupExpectedResponse(bool expectedSucessStatus,
            string expectedName, Cuisine expectedCuisine, int expectedRestId, IEnumerable<Review> expectedReviews,
            double expectedAverageRating, int expectedReviewCount)
        {
            return new ViewRestaurantResponseModel
            {
                WasSucessfull = expectedSucessStatus,
                Name = expectedName,
                CuisineId = expectedCuisine.Id,
                CuisineName = expectedCuisine.Name,
                RestaurantId = expectedRestId,
                Reviews = expectedReviews,
                AverageRating = expectedAverageRating,
                ReviewCount = expectedReviewCount
            };
        }

        private void ValidateRestaurant(int expectedRestId, string expectedName, Cuisine expectedCuisine,
            IEnumerable<Review> expectedReviews, double expectedAverageRating, int expectedReviewCount)
        {
            var actualRest = Restaurants.Find(r => r.Id == expectedRestId);
            Assert.IsNotNull(actualRest, "Update restaurant not found");
            Assert.AreEqual(expectedRestId, actualRest.Id, "Restaurant ID");
            Assert.AreEqual(expectedName, actualRest.Name, "Restaurant Name");
            Assert.AreEqual(expectedCuisine.Id, actualRest.Cuisine.Id, "Restaurant CuisineId");

            var reviews = expectedReviews.ToArray();
            var actulaReviews = actualRest.Reviews.ToArray();
            for (var i = 0; i < reviews.Length; i++)
                Assert.AreEqual(reviews[i], actulaReviews[i]);

            Assert.AreEqual(expectedAverageRating, actualRest.AverageRating);
            Assert.AreEqual(expectedReviewCount, actualRest.ReviewCount);
        }

        [TestMethod]
        public void ViewRestaurant_ValidIDOneReviews_Succeed()
        {
            var viewingUserId = 1;
            var viewingRestID = 2;
            var expectedRequestModel = SetupExpectedRequest(viewingUserId, viewingRestID);

            var expectedName = "Restaurant name Two";
            var expectedCuisine = Cuisines[0];
            var expectedRestId = 2;
            var expectedAverageRating = 3;
            var expectedReviewCount = 1;
            var expectedReviews = new List<Review>()
            {
                new Review
                {
                    Comment = "Comment for 2",
                    Rating = 3,
                    PostedDateTime = new DateTime(2016, 10, 16),
                    ReviewNumber = 1,
                    ReviewUser = Users[2]
                }
            };

            //TODO: review response to check if the values can be updated (they should not be able to)
            var expectedSucessStatus = true;
            var expectedResponse = SetupExpectedResponse(expectedSucessStatus, expectedName,
                expectedCuisine, expectedRestId, expectedReviews, expectedAverageRating, expectedReviewCount);

            var viewRestTran = new ViewRestaurantTransaction(Repo, Log, expectedRequestModel);

            viewRestTran.Execute();
            var actualResponse = viewRestTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateRestaurant(expectedRestId, expectedName, expectedCuisine, expectedReviews, expectedAverageRating,
                expectedReviewCount);
        }
        [TestMethod]
        public void ViewRestaurant_ValidIDTwoReviews_Succeed()
        {
            var viewingUserId = 1;
            var viewingRestID = 3;
            var expectedRequestModel = SetupExpectedRequest(viewingUserId, viewingRestID);

            var expectedName = "Restaurant name Three";
            var expectedCuisine = Cuisines[1];
            var expectedRestId = 3;
            var expectedAverageRating = 4;
            var expectedReviewCount = 2;
            var expectedReviews = new List<Review>()
            {
                new Review
                {
                    Comment = "First Comment for 3",
                    Rating = 3,
                    PostedDateTime = new DateTime(2016, 10, 16),
                    ReviewNumber = 2,
                    ReviewUser = Users[3]
                },
                new Review
                {
                    Comment = "Second Comment for 3",
                    Rating = 5,
                    PostedDateTime = new DateTime(2016, 10, 10),
                    ReviewNumber = 3,
                    ReviewUser = Users[2]
                }
            };

            var expectedSucessStatus = true;
            var expectedResponse = SetupExpectedResponse(expectedSucessStatus, expectedName,
                expectedCuisine, expectedRestId, expectedReviews, expectedAverageRating, expectedReviewCount);


            var viewRestTran = new ViewRestaurantTransaction(Repo, Log, expectedRequestModel);

            viewRestTran.Execute();
            var actualResponse = viewRestTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateRestaurant(expectedRestId, expectedName, expectedCuisine, expectedReviews, expectedAverageRating,
                expectedReviewCount);
        }
        [TestMethod]
        public void ViewRestaurant_ValidIdTenReviews_Succeed()
        {
            var viewingUserId = 2;
            var viewingRestID = 4;
            var expectedRequestModel = SetupExpectedRequest(viewingUserId, viewingRestID);

            var expectedName = "Restaurant name Four";
            var expectedCuisine = Cuisines[1];
            var expectedRestId = 4;
            var expectedAverageRating = 3;
            var expectedReviewCount = 10;
            var expectedReviews = new List<Review>();
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

                expectedReviews.Add(newReview);
            }

            var expectedSucessStatus = true;
            var expectedResponse = SetupExpectedResponse(expectedSucessStatus, expectedName,
                expectedCuisine, expectedRestId, expectedReviews, expectedAverageRating, expectedReviewCount);

            var viewRestTran = new ViewRestaurantTransaction(Repo, Log, expectedRequestModel);

            viewRestTran.Execute();
            var actualResponse = viewRestTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");

            ValidateRestaurant(expectedRestId, expectedName, expectedCuisine, expectedReviews, expectedAverageRating,
                expectedReviewCount);
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdMultiReviewsDuplicateUser_Succeed()
        {
        }
        [TestMethod]
        [Ignore]
        public void ViewRestaurantAvg_ValidIdReviewCountDuplicateUser_Succeed()
        {
        }
        [TestMethod]
        [ExpectedException(typeof(RestaurantNotFoundException))]
        public void ViewRestaurant_InvalidID_Fail()
        {
            var viewingUserId = 1;
            var viewingRestID = 20;
            var expectedRequestModel = SetupExpectedRequest(viewingUserId, viewingRestID);

            var expectedName = string.Empty;
            var expectedCuisine = string.Empty;
            var expectedRestId = 0;
            var expectedReviews = Enumerable.Empty<Review>();
            var expectedAverageRating = 0;
            var expectedReviewCount = 0;
            var expectedSucessStatus = false;

            var viewRestTran = new ViewRestaurantTransaction(Repo, Log, expectedRequestModel);

            viewRestTran.Execute();
            var actualResponse = viewRestTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
        }
    }
}
