using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace MySpecFlowTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private By usernameField = By.XPath("//input[@name='username']");
        private By passwordField = By.Name("password");
        private By loginButton = By.XPath("//button[@type='submit']");
        private By dashboardHeader = By.XPath("//h6[text()='Dashboard']");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public void Login(string email, string password)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(usernameField)).SendKeys(email);
            driver.FindElement(passwordField).SendKeys(password);
            driver.FindElement(loginButton).Click();

            // Wait for dashboard to ensure login success and page loaded
            wait.Until(ExpectedConditions.ElementIsVisible(dashboardHeader));
        }
    }
}
