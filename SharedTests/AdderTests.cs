using Shared.Utitlties;

namespace SharedTests
{

    [TestClass]
    public class AdderTests
    {
        [TestMethod]
        public void SumOf5Plus10ShouldBe15()
        {
            var classUnderTest = new Adder();
            classUnderTest.Num1 = 5.0f;
            classUnderTest.Num2 = 10.0f;
            float actualResult = classUnderTest.Sum;
            float expectedResult = 15.0f;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SumOfMinus5Plus10ShouldBe5()
        {
            var classUnderTest = new Adder();
            classUnderTest.Num1 = -5.0f;
            classUnderTest.Num2 = 10.0f;
            float actualResult = classUnderTest.Sum;
            float expectedResult = 5.0f;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}