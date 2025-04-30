using FlipkartTests.Pages;
using FlipkartTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;


namespace FlipkartTests.Tests
{
    [TestFixture]
    [Category("CartTests")]
    public class CartTests
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
        public void VerifyAddToCartFunctionality()
        {
            try
            {
                // Arrange
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();
                homePage.SearchProduct(testData.Products.SearchProduct1);

                // Act
                ProductPage productPage = new ProductPage(driver);
                string productTitle = productPage.GetProductTitle();
                productPage.AddToCart();

                CartPage cartPage = new CartPage(driver);
                int cartItemCount = cartPage.GetCartItemCount();

                // Assert
                Assert.AreEqual(1, cartItemCount, "Product was not added to cart");
                ReportUtility.LogPass($"Product '{productTitle}' added to cart successfully");
            }
            catch (Exception ex)
            {
                ReportUtility.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Order(2)]
        public void VerifyRemoveFromCartFunctionality()
        {
            try
            {
                // Arrange - Add item first
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();
                homePage.SearchProduct(testData.Products.SearchProduct1);

                ProductPage productPage = new ProductPage(driver);
                productPage.AddToCart();

                // Act
                CartPage cartPage = new CartPage(driver);
                cartPage.RemoveItemFromCart();

                // Assert
                Assert.IsTrue(cartPage.IsCartEmpty(), "Cart is not empty after removal");
                ReportUtility.LogPass("Item removed from cart successfully");
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
                driver.Dispose(); // Ensure the driver is disposed here
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