using FlipkartTests.Pages;
using FlipkartTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FlipkartTests.Tests
{
    [TestFixture]
    [Category("LoginTests")]
    public class LoginTest
    {
        private IWebDriver driver;
        private BrowserUtility browserUtility;
        private dynamic testData;

        [OneTimeSetUp]
        public void Setup()
        {
            ReportUtility.InitializeReport();
            browserUtility = new BrowserUtility();
            testData = TestDataReader.GetTestData();
        }

        [SetUp]
        public void StartTest()
        {
            driver = browserUtility.InitBrowser("chrome");
            browserUtility.NavigateToUrl(testData.Urls.BaseUrl);
            ReportUtility.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        [Order(1)]
        [Category("Smoke")]
        public void ValidLoginTest()
        {
            try
            {
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();

                LoginPage loginPage = homePage.ClickLogin();
                loginPage.EnterUsername(testData.ValidCredentials.Username);
                loginPage.EnterPassword(testData.ValidCredentials.Password);

                homePage = loginPage.ClickLogin();
                CommonElements commonElements = new CommonElements(driver);

                Assert.IsTrue(commonElements.IsLogoDisplayed(), "Login failed - Logo not displayed");
                ReportUtility.LogPass("Login successful with valid credentials");
            }
            catch (Exception ex)
            {
                ReportUtility.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Order(2)]
        public void InvalidLoginTest()
        {
            try
            {
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();

                LoginPage loginPage = homePage.ClickLogin();
                loginPage.EnterUsername(testData.InvalidCredentials.Username);
                loginPage.EnterPassword(testData.InvalidCredentials.Password);

                loginPage.ClickLogin();

                Assert.IsTrue(loginPage.IsErrorMessageDisplayed(), "Error message not displayed for invalid login");
                ReportUtility.LogPass("Error message displayed correctly for invalid login attempt");
            }
            catch (Exception ex)
            {
                ReportUtility.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void EndTest()
        {
            string screenshot = new CommonUtility(driver).CaptureScreenshot();
            ReportUtility.LogScreenshot(screenshot, "Test Screenshot");

            // Dispose of the driver to fix NUnit1032
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }

            browserUtility.CloseBrowser();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            ReportUtility.FlushReport();
        }
    }
}