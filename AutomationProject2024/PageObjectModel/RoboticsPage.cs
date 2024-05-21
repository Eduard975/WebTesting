using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutomationProject2024.PageObjectModel
{
    public class RoboticsPage
    {
        IWebDriver driver;

        public RoboticsPage(IWebDriver browser)
        {
            driver = browser;
        }


        IWebElement pageTitle => driver.FindElement(By.Id("page-title-heading"));

        IList<IWebElement> productsList => driver.FindElements(By.XPath("//li[contains(@class, 'ajax_block_product')]"));

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

        public ProductDetailsPage GoToProductDetails(int indexProduct)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 1000);");

            IWebElement linkElement = productsList.ElementAt(indexProduct).FindElement(By.XPath(".//a[@class='product_img_link']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(linkElement).Perform();

            Thread.Sleep(1000);
  
            string hrefValue = linkElement.GetAttribute("href");

            driver.Navigate().GoToUrl(hrefValue);

            return new ProductDetailsPage(driver);
        }

        public string GetProductName(int index)
        {
            var list=productsList.Count();
           Thread.Sleep(500);
           return productsList.ElementAt(index).FindElement(By.ClassName("product-name")).Text;
        }
    }
}
