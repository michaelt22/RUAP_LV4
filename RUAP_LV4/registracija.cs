using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Registarcija
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demowebshop.tricentis.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheRegistarcijaTest()
        {
            var rand = new Random();
            var i = rand.Next(1, 100000);
            var j = rand.Next(1, 100000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            driver.Navigate().GoToUrl(baseURL + "/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Register")));
            driver.FindElement(By.LinkText("Register")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gender-male")));
            driver.FindElement(By.Id("gender-male")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("FirstName")));
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("TestName" + i);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("LastName")));
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("TestName" + j);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("testuser" + i + "@test.com");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ConfirmPassword")));
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("123456");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("register-button")));
            driver.FindElement(By.Id("register-button")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
