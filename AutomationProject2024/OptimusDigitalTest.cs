using AutomationProject2024.PageObjectModel;
using AutomationProject2024.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
/*  to have these namespaces you need to add in solution 
from ManageNuGet Packages the following:
Selenium.Webdriver
Selenium.Webdriver.ChromeDriver
Selenium.Support*/

namespace AutomationProject2024
{
    [TestClass]
    public class OptimusDigitalTest
    {
        private ChromeDriver driver;
        private MenuItemsBeforeSignIn menuItemsBeforeSignIn;
        private LoginPage loginPage;
        private RegisterPage registerPage;

        private HomePage homePage;
        private CookieConsent cookieConsent;

        [TestInitialize]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.optimusdigital.ro/ro/");
            

            cookieConsent = new CookieConsent(driver);
            loginPage = new LoginPage(driver);
            menuItemsBeforeSignIn = new MenuItemsBeforeSignIn(driver);
            cookieConsent.GoToMenuAfterCookieAccept();
        }

        [TestMethod]
        public void LoginValidAccount()
        {
            
            menuItemsBeforeSignIn.GoToLogin();

            loginPage.SignInAccount(Resources.email, Resources.password);
            
            homePage = new HomePage(driver);
            
            Thread.Sleep(2000);
            Assert.IsTrue(homePage.GetWelcomeText().Contains(Resources.welcomeMessage), ValidationText.UnknownText);
        }

        //This test should be refactorized
        [TestMethod]
        public void Should_DisplayAlreadyCreatedAccountPage_When_TheAccountWasCreatedBefore()
        {
            menuItemsBeforeSignIn.GoToLogin();
            Thread.Sleep(1000);

            loginPage.tryRegisterEmail(Resources.email);

            Thread.Sleep(1000);
            Assert.IsTrue(loginPage.GetAlertText().Contains(Resources.emailAlreadyExists), ValidationText.UnknownText);
        }

        [TestMethod]
        public void RegisterNewAccount()
        {
            menuItemsBeforeSignIn.GoToLogin();
            Thread.Sleep(1000);

            loginPage.tryRegisterEmail(Resources.newEmail);

            Thread.Sleep(1000);

            registerPage = new RegisterPage(driver);
            registerPage = registerPage.FillInformation(Resources.gender, Resources.firstname, Resources.lastname, Resources.password, Resources.birthdate);

            Thread.Sleep(1000);
            Assert.IsTrue(registerPage.GetAlertText().Contains(Resources.SuccAccCreate), ValidationText.FailedToCreate);
        }

        [TestMethod]
        public void ShouldGoToProductDetails()
        {
            RoboticsPage roboticsPage = menuItemsBeforeSignIn.GoToRoboticsPage();

            Thread.Sleep(1000);

            Assert.IsTrue(roboticsPage.GetPageTitle().Equals(Resources.roboticsPageTitle), ValidationText.UnknownText);
           
            var detailsPageTitle = roboticsPage.GetProductName(0);
            roboticsPage.GoToProductDetails(0);
            ProductDetailsPage productDetails = new ProductDetailsPage(driver);
            Assert.IsTrue(productDetails.GetPageTitle().Equals(detailsPageTitle), ValidationText.UnknownText);
        }


        [TestMethod]
        public void AddValidProductInCart()
        {
            menuItemsBeforeSignIn.GoToRoboticsPage();
            RoboticsPage robotics;
            robotics = new RoboticsPage(driver);
            ProductDetailsPage roboticsDetailsPage = new ProductDetailsPage(driver);
            ShoppingCartPage shoppingCartPage;
            shoppingCartPage = new ShoppingCartPage(driver);

            var productName1 = robotics.GetProductName(1);
            robotics.GoToProductDetails(1);

            Assert.IsTrue(roboticsDetailsPage.GetPageTitle().Equals(productName1), "Is not equal");

            roboticsDetailsPage.AddProductToCart();

            roboticsDetailsPage.GoToShoppingCart();

            shoppingCartPage.ProceedToCheckoutPage();
        }

        [TestMethod]
        public void RemoveProductFromCart()
        {
            menuItemsBeforeSignIn.GoToRoboticsPage();
            RoboticsPage robotics;
            robotics = new RoboticsPage(driver);
            ProductDetailsPage roboticsDetailsPage = new ProductDetailsPage(driver);
            ShoppingCartPage shoppingCartPage;
            shoppingCartPage = new ShoppingCartPage(driver);

            var productName1 = robotics.GetProductName(1);
            robotics.GoToProductDetails(1);

            roboticsDetailsPage.AddProductToCart();

            shoppingCartPage = roboticsDetailsPage.GoToShoppingCart();

            int currentNumberOfItemsInCart = shoppingCartPage.getNumberOfItemsInShoppingCart();

            shoppingCartPage = shoppingCartPage.RemoveItemFromShoppingCart(0);

            int updatedNumberOfItemsInCart = shoppingCartPage.getNumberOfItemsInShoppingCart();


            Assert.IsTrue(currentNumberOfItemsInCart == updatedNumberOfItemsInCart + 1, "Is not Equal");
        }

        [TestCleanup]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}