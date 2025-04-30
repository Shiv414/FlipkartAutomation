using FlipkartTests.Pages;
using FlipkartTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FlipkartTests.Tests
{
    [TestFixture]
    [Category("CheckoutTests")]
    public class CheckoutTests
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
        public void VerifyCheckoutFlowForGuestUser()
        {
            try
            {
                // Arrange - Add product to cart
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();
                homePage.SearchProduct(testData.Products.SearchProduct1);

                ProductPage productPage = new ProductPage(driver);
                productPage.AddToCart();

                // Act - Navigate to checkout
                CartPage cartPage = new CartPage(driver);
                CheckoutPage checkoutPage = cartPage.ClickPlaceOrder();

                // Assert
                Assert.IsTrue(checkoutPage.IsDeliveryAddressDisplayed(), "Delivery address section not displayed");
                ReportUtility.LogPass("Checkout flow initiated successfully for guest user");
            }
            catch (Exception ex)
            {
                ReportUtility.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }

        [Test]
        [Order(2)]
        public void VerifyPaymentOptionsDisplayed()
        {
            try
            {
                // Arrange - Add product and proceed to checkout
                HomePage homePage = new HomePage(driver);
                homePage.CloseLoginPopup();
                homePage.SearchProduct(testData.Products.SearchProduct2);

                ProductPage productPage = new ProductPage(driver);
                productPage.AddToCart();

                CartPage cartPage = new CartPage(driver);
                CheckoutPage checkoutPage = cartPage.ClickPlaceOrder();
                checkoutPage.ClickContinue();

                // Assert
                Assert.IsTrue(checkoutPage.ArePaymentOptionsDisplayed(), "Payment options not displayed");
                ReportUtility.LogPass("Payment options displayed correctly in checkout flow");
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
                driver.Dispose(); // Ensure the driver is disposed
                driver = null; // Set to null to avoid potential reuse
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
