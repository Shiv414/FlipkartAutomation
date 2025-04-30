using FlipkartAutomation.Pages;
using OpenQA.Selenium;

namespace FlipkartTests.Pages
{
    public class LoginPage : BasePage
    {
        // Locators
        private By usernameField = By.XPath("//input[@type='text' and @class='_2IX_2- VJZDxU']");
        private By passwordField = By.XPath("//input[@type='password']");
        private By loginButton = By.XPath("//button[@type='submit' and span[text()='Login']]");
        private By errorMessage = By.XPath("//span[contains(text(),'Your username or password is incorrect')]");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void EnterUsername(string username)
        {
            SendKeys(usernameField, username);
        }

        public void EnterPassword(string password)
        {
            SendKeys(passwordField, password);
        }

        public HomePage ClickLogin()
        {
            Click(loginButton);
            return new HomePage(driver);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsElementPresent(errorMessage);
        }
    }
}