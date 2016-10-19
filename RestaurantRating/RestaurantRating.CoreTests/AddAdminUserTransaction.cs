using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantRating.Domain;

namespace RestaurantRating.DomainTests
{
    [TestClass]
    public class AddAdminUserTests : MockTestSetup
    {
        [TestMethod]
        public void AddAdmin_ValidInput_Succeed()
        {
            var expectedFirstName = "Ruchira";
            var expectedLastName = "Kumarasinghe";
            var expectedEmailAddress = "ruch911@yahoo.com";
            var expectedRequestModel = new AddAdminRequestModel {FirstName =expectedFirstName, LastName = expectedLastName, EmailAddress = expectedEmailAddress};

            var expectedUserId = 1;
            var expectedSucessStatus = true;
            var expectedResponse = new AddAdminResponseModel {WasSucessfull=expectedSucessStatus, UserId= expectedUserId};
            
            var addAdminTran = new AddAdminTransaction(Repo, Log, expectedRequestModel);

            addAdminTran.Execute();
            var actualResponse = addAdminTran.Response;

            //assert
            Assert.AreEqual(expectedSucessStatus, actualResponse.WasSucessfull, "Invalid execution status");
            Assert.AreEqual(expectedResponse, actualResponse, "Invalid response");
        }
    }
}
