using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class ViewCuisinesTests :MockTestSetup
    {
        public ViewCuisinesTests()
        {
            Cuisines.Add(new Cuisine { Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1});
            Cuisines.Add(new Cuisine { Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1});
            Cuisines.Add(new Cuisine { Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1});
            Cuisines.Add(new Cuisine { Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1});
            Cuisines.Add(new Cuisine { Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1});

        }

        [TestMethod]
        public void ViewCuisine_All_Succeed()
        {
            var viewingUserId = 1;
            var expectedRequestModel = new ViewCuisinesRequestModel { UserId = viewingUserId };
            var expectedSucessStatus = true;
            var expectedCuisineCount = 5;

            var expectedResponse = new[]
            {
                new Cuisine() {Id = 1, Name = "Indian", CreatedBy = 1, UpdatedBy = 1},
                new Cuisine() {Id = 2, Name = "Armenian", CreatedBy = 1, UpdatedBy = 1},
                new Cuisine() {Id = 3, Name = "Italian", CreatedBy = 1, UpdatedBy = 1},
                new Cuisine() {Id = 4, Name = "Cajun", CreatedBy = 2, UpdatedBy = 1},
                new Cuisine() {Id = 5, Name = "Mexican", CreatedBy = 2, UpdatedBy = 1}
            };

            var tran = new ViewCuisineTransaction(Repo, Log, expectedRequestModel);

            //act
            tran.Execute();
            var actualResponse = tran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            var expectedResponseIndex = 0;
            foreach (var actualCuisine in tran.Response.Cuisines)
            {
                var expectedCuisine = expectedResponse[expectedResponseIndex];

                Assert.AreEqual(actualCuisine.Id, expectedCuisine.Id);
                Assert.AreEqual(actualCuisine.Name, expectedCuisine.Name);

                expectedResponseIndex++;

            }
            Assert.AreEqual(expectedCuisineCount, expectedResponseIndex);
        }
    }
}
