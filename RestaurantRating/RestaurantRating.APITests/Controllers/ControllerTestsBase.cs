using Moq;
using RestaurantRating.Domain;

namespace RestaurantRating.APITests
{
    public class ControllerTestsBase
    {
        protected readonly Mock<IRepository> MockRepository = new Mock<IRepository>();
        protected readonly Mock<IApplicationLog> MockLogger = new Mock<IApplicationLog>();
        protected readonly int CallingUserId = 10;
    }
}