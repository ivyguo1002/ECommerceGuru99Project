using Magento.Utils;
using OpenQA.Selenium;
using System;

namespace Magento.Pages
{
    internal class BackendLoginPage
    {
        public IWebDriver Driver { get; set; }
        IWebElement UsernameTextBox => Driver.WaitForElement(By.Id("username"));
        IWebElement PasswordTextBox => Driver.WaitForElement(By.Id("login"));
        IWebElement LoginBtn => Driver.WaitForElement(By.CssSelector("input.form-button"));

        public BackendLoginPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal AdminPage Login(string username, string password)
        {
            UsernameTextBox.SendKeys(username);
            PasswordTextBox.SendKeys(password);
            LoginBtn.Click();
            return new AdminPage(Driver);
        }
    }
}