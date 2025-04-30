using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FlipkartTests.Utilities
{
    public class CommonUtility
    {
        private IWebDriver driver;

        public CommonUtility(IWebDriver driver)
        {
            this.driver = driver;
        }

        public List<string> FindBrokenLinks()
        {
            List<string> brokenLinks = new List<string>();
            var links = driver.FindElements(By.TagName("a"));

            foreach (var link in links)
            {
                string url = link.GetAttribute("href");
                if (!string.IsNullOrEmpty(url) && !IsLinkWorking(url))
                {
                    brokenLinks.Add(url);
                }
            }

            return brokenLinks;
        }

        private bool IsLinkWorking(string url)
        {
            // Implement actual link checking logic
            // For now returning true as dummy implementation
            return true;
        }

        public string CaptureScreenshot()
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            return ts.GetScreenshot().AsBase64EncodedString;
        }
    }
}