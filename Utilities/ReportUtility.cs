using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;

namespace FlipkartTests.Utilities
{
    public class ReportUtility
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter(TestContext.CurrentContext.TestDirectory + "\\TestReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Host Name", "Flipkart Test Automation");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("User Name", "Automation Team");
        }

        public static void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public static void LogInfo(string message)
        {
            test.Info(message);
        }

        public static void LogPass(string message)
        {
            test.Pass(message);
        }

        public static void LogFail(string message)
        {
            test.Fail(message);
        }

       // public static void LogScreenshot(string base64Image, string title)
        /*{
            test.AddScreenCaptureFromBase64String(base64Image, title);
        }*/

        public static void FlushReport()
        {
            extent.Flush();
        }

        internal static void LogScreenshot(string screenshot, string v)
        {
            throw new NotImplementedException();
        }
    }
}