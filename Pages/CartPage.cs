using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class CartPage : BasePage
    {
        // Locators
        private By cartItems = By.XPath("//div[contains(@class,'_1AtVbE')]");
        private By placeOrderButton = By.XPath("//button/span[text()='Place Order']");
        private By removeItemButton = By.XPath("//div[text()='Remove']");
        private By emptyCartMessage = By.XPath("//div[contains(text(),'Your cart is empty!')]");

        public CartPage(IWebDriver driver) : base(driver) { }

        public int GetCartItemCount()
        {
            return driver.FindElements(cartItems).Count;
        }

        public CheckoutPage ClickPlaceOrder()
        {
            Click(placeOrderButton);
            return new CheckoutPage(driver);
        }

        public void RemoveItemFromCart()
        {
            Click(removeItemButton);
        }

        public bool IsCartEmpty()
        {
            return IsElementPresent(emptyCartMessage);
        }
    }
}