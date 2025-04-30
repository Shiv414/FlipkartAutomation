using FlipkartTests.Pages;
using FlipkartTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FlipkartTests.Tests
{
    [TestFixture]
    [Category("ProductTests")]
    public class ProductTests
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
        public void VerifyProductSearchFunctionality()
        {
            try
            {
                // Arrange
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();

                // Act
                homePage.SearchProduct(testData.Products.SearchProduct1);
                ProductPage productPage = new ProductPage(driver);
                string productTitle = productPage.GetProductTitle();

                // Assert
                Assert.IsNotEmpty(productTitle, "Product title is empty");
              //  Assert.IsTrue(productTitle.Contains(testData.Products.SearchProduct1.ToString(),
                   // $"Product title '{productTitle}' doesn't match search term"));
                ReportUtility.LogPass($"Product '{productTitle}' found successfully");
            }
            catch (Exception ex)
            {
                ReportUtility.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Order(2)]
        public void VerifyProductDetailsPageLoads()
        {
            try
            {
                // Arrange
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();
                homePage.SearchProduct(testData.Products.SearchProduct2);

                // Act
                ProductPage productPage = new ProductPage(driver);
                bool isBuyNowVisible = productPage.IsElementPresent(productPage.BuyNowButton);
                bool isAddToCartVisible = productPage.IsElementPresent(productPage.AddToCartButton);

                // Assert
                Assert.IsTrue(isBuyNowVisible, "Buy Now button not visible");
                Assert.IsTrue(isAddToCartVisible, "Add to Cart button not visible");
                ReportUtility.LogPass("Product details page loaded with all key elements");
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