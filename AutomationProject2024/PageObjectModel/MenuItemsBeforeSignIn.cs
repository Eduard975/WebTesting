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

        public IWebElement roboticsPageOption => driver.FindElement(By.XPath("//li//a[@title=''][contains(text(), 'Robotică')]"));

        public RoboticsPage GoToRoboticsPage()
        {
            Actions actions = new Actions(driver);
            
            actions.MoveToElement(roboticsPageOption);
            Thread.Sleep(1000);
            roboticsPageOption.Click();

            return new RoboticsPage(driver);
        }

        public LoginPage GoToLogin()
        {
            linkSign.Click();

            return new LoginPage(driver);
        }
    }
}
