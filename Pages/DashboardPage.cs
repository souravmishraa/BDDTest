using OpenQA.Selenium;

class DashboardPage
{
    private readonly IWebDriver driver;

    public DashboardPage(IWebDriver driver) => this.driver = driver;

    private By pimfield = By.XPath("//span[text()='PIM']");
    private By addemployee = By.XPath("//a[text()='Add Employee']");
    //find Add employee and click on Add Employee option
		private By firstNameField = By.XPath("//input[@name='firstName']");
    private By lastNameField = By.XPath("//input[@name='lastName']");
    private By saveButton = By.XPath("//button[text()=' Save ']");
    private By dashboard = By.XPath("//span[text()='Dashboard']");
    //span[@class='oxd-userdropdown-tab']
        private By tabclick = By.XPath("//span[@class='oxd-userdropdown-tab']");



    public void ClickPIM()
    {
        driver.FindElement(pimfield).Click();
    }

    public void ClickAddEmployee()
    {
        driver.FindElement(addemployee).Click();
    }
      // Enter First Name
    public void EnterFirstName(string firstName)
{
    Thread.Sleep(5000); // 5 seconds wait
    driver.FindElement(firstNameField).SendKeys(firstName);
}

    // Enter Last Name
    public void EnterLastName(string lastName)
    {
        driver.FindElement(lastNameField).SendKeys(lastName);
    }

   // Click Save Button
    public void ClickSave()
    {
        driver.FindElement(saveButton).Click();
    }
    // Click on Dashboard window Handling
    public void ClickDashboard()
{
    string dashboardUrl = "https://opensource-demo.orangehrmlive.com/web/index.php/dashboard/index";
    string script = $"window.open('{dashboardUrl}', '_blank');";
    ((IJavaScriptExecutor)driver).ExecuteScript(script);
}
// Click on Dashboard window Handling
public void clickonprofile()
    {
        driver.FindElement(tabclick).Click();
    }

    internal void ClickOnProfile()
    {
        throw new NotImplementedException();
    }
}
