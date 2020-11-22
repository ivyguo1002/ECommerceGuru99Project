using Magento.Config;
using Magento.DataModel;
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
    //Verify you can create account in E-commerce site and can share wish list to other people using email
    //Product = LGLCD
    //Go to http://live.demoguru99.com/
    //Click on my account link
    //Click Create Account link and fill New User information except Email ID
    //Click Register
    //Verify Registration is done - Assert
    //Go to TV menu
    //Add product in your wish list
    //Click Share Wishlist
    //In next page enter Email and a message and click Share Wishlist
    //Check wishlist is shared – Assert

    public class Task5 : BaseTest
    {

        [Test]
        [TestCase("test", "qa", "fakeUser@gmail.com", "123456", "Thank you for registering with Main Website Store.", "LG LCD")]
        public void UserShouldBeAbleToCreateAccountAndShareWishList(string firstName, string lastName, string email, string password, string msg, string product)
        {
            Driver.Navigate().GoToUrl(MagentoConfig.HomePage);
            var homePage = new HomePage(Driver);

            var accountPage = homePage.GoToCreateAccountPage();
            var testUser = new UserInfo()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                Password = password
            };
            accountPage.RegisterWith(testUser);

            //Verify Registration is done - Assert
            Assert.That(accountPage.GetSuccessMsg(), Does.Contain(msg));

            var tvPage = homePage.GoToTVPage();
            var wishlistPage = tvPage.AddToWishList(product);
           // wishlistPage.ShareWishList();

        }



    }
}
