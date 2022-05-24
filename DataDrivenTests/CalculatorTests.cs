using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace DataDrivenTestingCalculator
{
    public class WebDriverCalculatorTests
    {
        private ChromeDriver driver;
        
        IWebElement field1;
        IWebElement field2;
        IWebElement operation;
        IWebElement calculate;
        IWebElement resultField;
        IWebElement clearField;

        [OneTimeSetUp]
        public void OpenAndNavigate()
        {
            this.driver = new ChromeDriver();
           
            driver.Url = "https://number-calculator.nakov.repl.co/";
            field1 = driver.FindElement(By.Id("number1"));
            field2 = driver.FindElement(By.Id("number2"));
            operation = driver.FindElement(By.Id("operation"));
            calculate = driver.FindElement(By.Id("calcButton"));
            resultField = driver.FindElement(By.Id("result"));
            clearField = driver.FindElement(By.Id("resetButton"));
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
        // Test valid integers
        [TestCase("5", "3", "+", "Result: 8")]
        [TestCase("5", "3", "-", "Result: 2")]
        [TestCase("5", "3", "*", "Result: 15")]
        [TestCase("12", "3", "/", "Result: 4")]

        // Test valid decimals
        [TestCase("5.23", "3.88", "+", "Result: 9.11")]
        [TestCase("3.14", "12.763", "-", "Result: -9.623")]
        [TestCase("3.14", "-7.534", "*", "Result: -23.65676")]
        [TestCase("12.5", "4", "/", "Result: 3.125")]

        // Tests with exponential numbers 
        [TestCase("1.5e53", "150", "*", "Result: 2.25e+55")]
        [TestCase("1.5e53", "150", "/", "Result: 1e+51")]

        //Tests with invalid inputs
        [TestCase("", "3", "+", "Result: invalid input")]
        [TestCase("", "3", "-", "Result: invalid input")]
        [TestCase("", "3", "*", "Result: invalid input")]
        [TestCase("", "3", "/", "Result: invalid input")]
        [TestCase("5", "", "+", "Result: invalid input")]
        [TestCase("5", "", "-", "Result: invalid input")]
        [TestCase("5", "", "*", "Result: invalid input")]
        [TestCase("5", "", "/", "Result: invalid input")]
        [TestCase("asdas", "20", "*", "Result: invalid input")]
        [TestCase("5.5", "asdas", "+", "Result: invalid input")]
        [TestCase("asdjasdjasd", "jkljkhfg", "+", "Result: invalid input")]

        //Tests with invalid operations
        [TestCase("5", "7", "@", "Result: invalid operation")]
        [TestCase("5", "7", "", "Result: invalid operation")]
        [TestCase("5", "7", "!!!!!", "Result: invalid operation")]

        // Tests with Infinity
        [TestCase("Infinity", "1", "+", "Result: Infinity")]
        [TestCase("Infinity", "1", "-", "Result: Infinity")]
        [TestCase("Infinity", "1", "*", "Result: Infinity")]
        [TestCase("Infinity", "1", "/", "Result: Infinity")]
        [TestCase("1", "Infinity", "+", "Result: Infinity")]
        [TestCase("2", "Infinity", "-", "Result: -Infinity")]
        [TestCase("3", "Infinity", "*", "Result: Infinity")]
        [TestCase("4", "Infinity", "/", "Result: 0")]
        [TestCase("Infinity", "Infinity", "+", "Result: Infinity")]
        [TestCase("Infinity", "Infinity", "-", "Result: invalid calculation")]
        [TestCase("Infinity", "Infinity", "*", "Result: Infinity")]
        [TestCase("Infinity", "Infinity", "/", "Result: invalid calculation")]

        public void Test_Calculator(string num1, string num2, string operato, string result)
        {

            // Act
            field1.SendKeys(num1);
            operation.SendKeys(operato);
            field2.SendKeys(num2);

            calculate.Click();

            Assert.That(result, Is.EqualTo(resultField.Text));

            clearField.Click();

        }
    }
}