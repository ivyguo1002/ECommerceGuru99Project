using Magento.DataModel;
using Magento.Utils;
using OpenQA.Selenium;
using System;

namespace Magento.Pages
{
    internal class AccountPage
    {
        public IWebDriver Driver { get; set; }
        IWebElement FirstNameTextBox => Driver.WaitForElement(By.CssSelector("#firstname"));
        IWebElement LastNameTextBox => Driver.WaitForElement(By.CssSelector("#lastname"));
        IWebElement EmailTextBox => Driver.WaitForElement(By.CssSelector("#email_address"));
        IWebElement PasswordTextBox => Driver.WaitForElement(By.CssSelector("#password"));
        IWebElement ConfirmPWTextBox => Driver.WaitForElement(By.CssSelector("#confirmation"));

        IWebElement RegisterLink => Driver.WaitForElement(By.CssSelector("button[title='Register']"));
        IWebElement SuccessMsg => Driver.WaitForElement(By.CssSelector(".success-msg"));
        public AccountPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal void RegisterWith(UserInfo testUser)
        {
            FirstNameTextBox.SendKeys(testUser.FirstName);
            LastNameTextBox.SendKeys(testUser.LastName);
            EmailTextBox.SendKeys(testUser.EmailAddress);
            PasswordTextBox.SendKeys(testUser.Password);
            ConfirmPWTextBox.SendKeys(testUser.Password);
            RegisterLink.Click();
        }

        internal string GetSuccessMsg()
        {
            return SuccessMsg.Text;
        }
    }
}