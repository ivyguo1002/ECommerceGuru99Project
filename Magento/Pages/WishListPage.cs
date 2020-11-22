using Magento.Utils;
using OpenQA.Selenium;
using System;

namespace Magento.Pages
{
    internal class WishListPage
    {
        public IWebDriver Driver { get; set; }
        IWebElement ShareWishListBtn => Driver.WaitForElement(By.Name("save_and_share"));
        IWebElement EmailTextArea => Driver.WaitForElement(By.CssSelector("#email_address"));
        IWebElement MsgTextArea => Driver.WaitForElement(By.CssSelector("#message"));
        ///IWebElement Share
        public WishListPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal void ShareWishList(SharingInfo sharingInfo)
        {
            ShareWishListBtn.Click();
            EmailTextArea.SendKeys(sharingInfo.Email);
            MsgTextArea.SendKeys(sharingInfo.Message);

        }
    }
}