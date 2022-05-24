using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NunitWebDriverTests
{
    public class SoftUniTests
    {
        private WebDriver driver;

        [OneTimeSetUp]
        public void OpenBrowserAndNavigate()
        {
            // Add option to chrome browse instance
            var options = new ChromeOptions();
            // options.AddArguments("--headless", "--window-size=1920,1200");

            this.driver = new ChromeDriver(options);
            driver.Url = "https://softuni.bg";
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test_AssertMainPageTitle()
        {
            // Act
            driver.Url = "https://softuni.bg";
            string expectedTitle = "Обучение по програмиране - Софтуерен университет";

            // Assert
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_AssertAboutUsTitle()
        {
            // Act
            var zaNasElement = driver.FindElement(By.CssSelector("li:nth-of-type(1) > .nav-link > .cell"));
            zaNasElement.Click();

            string expectedTitle = "За нас - Софтуерен университет";

            // Assert
            Assert.That(driver.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void Test_Login_InvalidUsernameAndPassword()
        {
            driver.FindElement(By.CssSelector(".softuni-btn-primary")).Click();

            // Locate username field by ID
            // var usernmameField = driver.FindElement(By.Id("username"));

            // Locate username field by TagName
            var usernmameField_ByName = driver.FindElement(By.Name("username"));
            var usernmameField_CSSSelector = driver.FindElement(By.CssSelector("#username"));

            // IWebElement usernmameField = driver.FindElement(By.Id("username"));

            usernmameField_CSSSelector.Click();
            usernmameField_CSSSelector.SendKeys("user1");
            // usernmameField_CSSSelector.Clear();
            // driver.FindElement(By.Id("username")).SendKeys("user1");

            // usernmameField_CSSSelector.SendKeys(Keys.Tab);

            driver.FindElement(By.Id("password-input")).Click();
            driver.FindElement(By.Id("password-input")).SendKeys("user1");

            driver.FindElement(By.CssSelector(".softuni-btn")).Click();

            Assert.That(driver.FindElement(By.CssSelector("li")).Text, Is.EqualTo("Невалидно потребителско име или парола"));
        }


        [Test]
        public void Test_Search_PositiveResults()
        {
            driver.Url = "https://softuni.bg";
            // Click on Search button
            driver.FindElement(By.CssSelector(".header-search-dropdown-link .fa-search")).Click();

            // Type search value and hit Enter
            var searchBox = driver.FindElement(By.CssSelector(".container > form #search-input"));
            searchBox.Click();
            searchBox.SendKeys("QA");
            searchBox.SendKeys(Keys.Enter);

            var resultField = driver.FindElement(By.CssSelector(".search-title")).Text;

            var expectedVaue = "Резултати от търсене на “QA”";

            Assert.That(resultField, Is.EqualTo(expectedVaue));
        }
    }
}