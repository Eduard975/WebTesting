using System.Threading;
using OpenQA.Selenium;

namespace AutomationProject2024.PageObjectModel
{
    public class ProductDetailsPage
    {

        IWebDriver driver;

        public ProductDetailsPage(IWebDriver browser)
        {
            driver = browser;
        }

        IWebElement pageTitle => driver.FindElement(By.XPath("//h1[@itemprop='name']"));

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }

        public IWebElement BtnAddToCart => driver.FindElement(By.XPath("//button[@type='submit' and @name='Submit' and contains(@class, 'exclusive')]"));

        public ProductDetailsPage AddProductToCart()
        {
            BtnAddToCart.Click();

            return this;
        }

        public IWebElement ShoppingCartLink => driver.FindElement(By.XPath("//a[contains(@title, 'Afișare')]"));

        public ShoppingCartPage GoToShoppingCart()
        {
            //span[@class='continue btn btn-default button exclusive-medium' and @title='Continuaţi cumpărăturile']
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='continue btn btn-default button exclusive-medium' and @title='Continuaţi cumpărăturile']\r\n")).Click();
            Thread.Sleep(2000);
            ShoppingCartLink.Click();

            return new ShoppingCartPage(driver);
        }

    }
}
