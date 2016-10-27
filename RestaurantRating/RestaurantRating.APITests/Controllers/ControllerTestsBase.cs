using Moq;
using RestaurantRating.API;
using RestaurantRating.Domain;

namespace RestaurantRating.APITests
{
    public class ControllerTestsBase
    {
        protected readonly Mock<IRepository> MockRepository = new Mock<IRepository>();
        protected readonly Mock<IApplicationLog> MockLogger = new Mock<IApplicationLog>();
        protected readonly Mock<IIdentityProvider> MockIdentity = new Mock<IIdentityProvider>();

        public ControllerTestsBase()
        {
            MockIdentity.Setup(m => m.GetRequestingUserId()).Returns(10);
        }

    }
}