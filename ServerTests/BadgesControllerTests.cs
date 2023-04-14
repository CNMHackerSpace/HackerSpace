using Microsoft.Extensions.Logging;
using Server.Controllers;
using Server.Data.Mocks;

namespace ServerTests
{
    [TestClass]
    public class BadgesControllerTests
    {
        [TestMethod]
        public async Task ShouldGet5BadgesAsync()
        {
            var classUnderTest = new BadgesController(new LoggerMock<BadgesController>(),new BadgesRepoMock());

            var badges = await classUnderTest.GetBadges();
            int actualResult = badges.Count();
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}