using FlipkartTests.Tests;
using NUnit.Framework;

namespace FlipkartTests.Tests.SmokeTests
{
    [TestFixture]
    [Category("SmokeSuite")]
    public class SmokeTestSuite
    {
        [Test]
        [Order(1)]
        public void VerifyHomePageLoads()
        {
            // Implementation
        }

        [Test]
        [Order(2)]
        public void VerifyLoginFunctionality()
        {
            LoginTest loginTests = new LoginTest();
            loginTests.ValidLoginTest();
        }

        [Test]
        [Order(3)]
        public void VerifySearchFunctionality()
        {
            // Implementation
        }
    }
}