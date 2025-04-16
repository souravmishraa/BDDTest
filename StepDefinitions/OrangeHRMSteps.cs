using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using TechTalk.SpecFlow;
using MySpecFlowTests.Utilities;
using MySpecFlowTests.Pages;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;


namespace MySpecFlowTests.StepDefinitions
{
    [Binding]
    public class OrangeHRMSteps
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [BeforeScenario]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            driver.Quit();
        }

        //  CSV support: commented but preserved
        /*
        [Given(@"I login and add employee using data from CSV file")]
        public void GivenILoginAndAddEmployeeUsingDataFromCSVFile()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "LoginData.csv");
            var data = CSVReader.ReadCSV(filePath);

            foreach (var row in data)
            {
                string username = row["username"];
                string password = row["password"];
                string firstName = row["firstName"];
                string lastName = row["lastName"];

                Console.WriteLine($"Running test with: {username}, {password}, {firstName}, {lastName}");

                driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
                loginPage.Login(username, password);
                Assert.That(driver.Title, Is.EqualTo("OrangeHRM"));

                dashboardPage.ClickPIM();
                dashboardPage.ClickAddEmployee();
                dashboardPage.EnterFirstName(firstName);
                dashboardPage.EnterLastName(lastName);
            }
        }
        */

        // Excel support added below
        [Given(@"I login and add employee using data from Excel")]
        public void GivenILoginAndAddEmployeeUsingDataFromExcel()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "EmployeeData.xlsx");
            var data = ExcelReader.ReadExcel(filePath);

            foreach (var row in data)
            {
                string username = row["username"];
                string password = row["password"];
                string firstName = row["firstName"];
                string lastName = row["lastName"];

                Console.WriteLine($"Running test with: {username}, {password}, {firstName}, {lastName}");

                driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/");
                loginPage.Login(username, password);
                Assert.That(driver.Title, Is.EqualTo("OrangeHRM"));

                dashboardPage.ClickPIM();
                dashboardPage.ClickAddEmployee();
                dashboardPage.EnterFirstName(firstName);
                dashboardPage.EnterLastName(lastName);
                dashboardPage.ClickSave();
                Thread.Sleep(5000);
                // Switch to the new tab

                dashboardPage.ClickDashboard();
                var tabs = driver.WindowHandles;
                driver.SwitchTo().Window(tabs[tabs.Count - 1]);
                 dashboardPage.clickonprofile();
                Thread.Sleep(3000);

            }
        }
        // popup handling
//  [Given(@"I handle a JavaScript alert popup")]
// public void GivenIHandleAJavaScriptAlertPopup()
// {
//     driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
//     Thread.Sleep(2000); // Wait for page to load

//     // Wait for and click the alert button
//     var alertButton = driver.FindElement(By.XPath("//button[text()='Click for JS Alert']"));
//     Assert.That(alertButton.Displayed, Is.True, "Alert button is not visible.");
//     Thread.Sleep(1000); // Pause before clicking
//     alertButton.Click();

//     Thread.Sleep(1000); // Wait for alert to appear
//     IAlert alert = driver.SwitchTo().Alert();
//     string alertText = alert.Text;
//     Console.WriteLine($"Alert Text: {alertText}");

//     Assert.That(alertText, Is.EqualTo("I am a JS Alert"), "Alert text does not match expected.");
//     alert.Accept(); // Click OK

//     Thread.Sleep(1000); // Wait for result to appear
//     var result = driver.FindElement(By.Id("result"));
//     string resultText = result.Text;
//     Console.WriteLine($"Result Text: {resultText}");

//     Assert.That(resultText, Is.EqualTo("You successfully clicked an alert"), "Result message mismatch.");
// }

    }
}

