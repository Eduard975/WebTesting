using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutomationProject2024.PageObjectModel
{
    public class MenuItemsBeforeSignIn : MenuItems
    {
        private IWebDriver driver;

        public MenuItemsBeforeSignIn(IWebDriver browser): base(browser)
        {
            driver = browser;
        }

        public IWebElement linkSign => driver.FindElement(By.LinkText("Autentificare"));

        public IWebElement productsOption => driver.FindElement(By.XPath("//div[@id='block_top_menu']//a[@title='Produse']"));

        //public IWebElement roboticsPageOption => driver.FindElement(By.XPath("//li[@class='']//a[@title='Robotică']"));
     
        public WatchesPage GoToRoboticsPage()
        {
            Actions actions = new Actions(driver);
            
            actions.MoveToElement(productsOption);
            Thread.Sleep(1000);
            //roboticsPageOption.Click();

            return new WatchesPage(driver);
        }

        public LoginPage GoToLogin()
        {
            linkSign.Click();

            return new LoginPage(driver);
        }
    }
}
