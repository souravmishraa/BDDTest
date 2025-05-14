using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Threading;

namespace MySpecFlowTests
{
    [TestFixture("chrome")]
    [TestFixture("edge")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ParallelTesting
    {
        private RemoteWebDriver driver;
        private readonly string browser;
        private readonly string gridUrl = "http://192.168.31.151:4444";

        public ParallelTesting(string browser)
        {
            this.browser = browser;
        }

        [SetUp]
        public void Setup()
        {
            if (browser == "chrome")
            {
                var options = new ChromeOptions();
                driver = new RemoteWebDriver(new Uri(gridUrl), options);
            }
            else if (browser == "edge")
            {
                var options = new EdgeOptions();
                driver = new RemoteWebDriver(new Uri(gridUrl), options);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void OpenGoogleDotCom()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(10000); // To observe session on Grid
            Assert.That(driver.Title, Is.EqualTo("Google"));
        }

        [Test]
        public void OpenWikipediaDotOrg()
        {
            driver.Navigate().GoToUrl("https://www.wikipedia.org");
            Thread.Sleep(10000); // To observe session on Grid
            Assert.That(driver.Title, Does.Contain("Wikipedia"));
        }

        [TearDown]
        public void Teardown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
