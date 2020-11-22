using Magento.Config;
using Magento.Enums;
using Magento.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magento.Tests
{
    class Task4 : BaseTest
    {


        [Test]
        [TestCase("Sony Xperia", "IPhone")]
        public void UserShouldBeAbleToCompareProducts(string model1, string model2)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);
            var mobilePage = homePage.NavigateToMobilePage();

            mobilePage.AddToCompare(model1)
                .AddToCompare(model2)
                .Compare();
            string currentWindow = Driver.CurrentWindowHandle;
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                wait.Until(driver => driver.WindowHandles.Count == 2);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The Compare Window didn't pop up");
            }

            foreach (var windowHandle in Driver.WindowHandles)
            {
                if (!windowHandle.Equals(currentWindow))
                {
                    Driver.SwitchTo().Window(windowHandle);
                }
            }

            var comparePage = new ComparePage(Driver);
            Assert.That(comparePage.GetProductsList(), Is.EqualTo(new List<string>() { "SONY XPERIA", "IPHONE" }));
            comparePage.CloseWindow();
            try
            {
                wait.Until(driver => driver.WindowHandles.Count == 1);
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail("The Compare Window didn't close");
            }
        }
    }
}
