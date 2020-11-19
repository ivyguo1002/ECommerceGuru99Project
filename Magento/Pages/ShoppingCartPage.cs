using Magento.Utils;
using OpenQA.Selenium;
using System;

namespace Magento.Pages
{
    internal class ShoppingCartPage
    {
        public IWebDriver Driver { get; set; }
        private IWebElement QTYTextBox => Driver.WaitForElement(By.CssSelector("td.product-cart-actions input.qty.input-text"));
        private IWebElement UpdateBtn => Driver.WaitForElement(By.CssSelector("button[title='Update']"));
        private IWebElement ItemMsg => Driver.WaitForElement(By.CssSelector(".error"));

        private IWebElement EmptyCartBtn => Driver.WaitForElement(By.CssSelector("#empty_cart_button"));
        private IWebElement PageTitle => Driver.WaitForElement(By.CssSelector(".page-title"));
        private IWebElement FloatingViedoIframe => Driver.WaitForElement(By.CssSelector("iframe#flow_close_btn_iframe"));
        private IWebElement FloatingCloseBtn => Driver.WaitForElement(By.Id("closeBtn"));
        public ShoppingCartPage(IWebDriver driver)
        {
            Driver = driver;
        }

        internal void EditQTY(int qty)
        {
            QTYTextBox.Click();
            QTYTextBox.Clear();
            QTYTextBox.SendKeys(qty.ToString());
            UpdateBtn.Click();
        }

        internal string GetItemMsgTxt()
        {
            return ItemMsg.Text;
        }

        internal void EmptyCart()
        {
            EmptyCartBtn.Click();
        }

        internal string GetPageTitle()
        {
            return PageTitle.Text;
        }

        internal void CloseFloatingVideo()
        {
            Driver.SwitchTo().Frame(FloatingViedoIframe);
            FloatingCloseBtn.Click();
            Driver.SwitchTo().DefaultContent();
        }
    }
}