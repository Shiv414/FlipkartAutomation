using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class ProductPage : BasePage
    {
        // Locators
        private By productTitle = By.XPath("//span[@class='B_NuCI']");
        private By addToCartButton = By.XPath("//button[text()='ADD TO CART']");
        private By buyNowButton = By.XPath("//button[text()='BUY NOW']");

        public object BuyNowButton { get; internal set; }
        public object AddToCartButton { get; internal set; }

        public ProductPage(IWebDriver driver) : base(driver) { }

        public string GetProductTitle()
        {
            return GetText(productTitle);
        }

        public void AddToCart()
        {
            Click(addToCartButton);
        }

        public CheckoutPage BuyNow()
        {
            Click(buyNowButton);
            return new CheckoutPage(driver);
        }

        // Fix: Removed duplicate method and updated the existing one to use the correct parameter type
        internal bool IsElementPresent(By locator)
        {
            return base.IsElementPresent(locator);
        }

        internal bool IsElementPresent(object buyNowButton)
        {
            throw new NotImplementedException();
        }
    }
}