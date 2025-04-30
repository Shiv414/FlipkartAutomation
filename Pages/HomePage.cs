using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class HomePage : BasePage
    {
        // Locators
        private By loginLink = By.XPath("//a[text()='Login']");
        private By searchBox = By.Name("q");
        private By searchButton = By.XPath("//button[@type='submit']");
        private By cartIcon = By.XPath("//a[contains(text(),'Cart')]");
        private By popupCloseButton = By.XPath("//button[text()='✕']");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void CloseLoginPopup()
        {
            if (IsElementPresent(popupCloseButton))
            {
                Click(popupCloseButton);
            }
        }

        public LoginPage ClickLogin()
        {
            Click(loginLink);
            return new LoginPage(driver);
        }

        public void SearchProduct(string productName)
        {
            SendKeys(searchBox, productName);
            Click(searchButton);
        }

        public CartPage NavigateToCart()
        {
            Click(cartIcon);
            return new CartPage(driver);
        }
    }
}