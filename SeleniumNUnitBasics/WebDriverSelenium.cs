using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumNUnitBasics
{ 
    public class WebDriverSelenium
    {
     

        ChromeDriver driver = new ChromeDriver();
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver.Manage().Window.Maximize();
            driver.Url = "https://nakov.com";
        }
       

        [Test]
        public void Test_NakovCom_Title()
        {

            var windowTitle = driver.Title;
            Assert.That(windowTitle.Contains("Svetlin Nakov - Svetlin Nakov – Official Web Site and Blog"));

        }

        [Test]
        public void Test_SearchNakovCom_for_QA()
        {
            driver.Navigate().GoToUrl("https://nakov.com/");
            driver.FindElement(By.Id("s")).Click();
            driver.FindElement(By.Id("s")).SendKeys("QA");
            driver.FindElement(By.Id("searchsubmit")).Click();
            Assert.That(
              driver.FindElement(By.CssSelector(".entry-title")).Text,
              Is.EqualTo(@"Search Results for – ""QA"""));
        }



        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}