using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace AutomationProject2024.PageObjectModel
{
    public class ShoppingCartPage
    {
        private IWebDriver driver;

        public ShoppingCartPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnProceedToCheckout => 
            driver.FindElement(By.XPath("//a[contains(@class, 'standard-checkout')]"));


        IList<IWebElement> productsList => 
            driver.FindElements(By.XPath("//tr[contains(@class, 'cart_item')]"));

        public ShippingAddressPage ProceedToCheckoutPage()
        {
            // Era o reclama deasupra butonului de checkout
            // Folosim JS sa navigam pana in partea de jos a paginii unde butonul nu mai este acoperit
            // alta solutie ar fi fost sa inchidem reclama
            // https://stackoverflow.com/questions/18833064/scroll-the-page-almost-to-the-end-in-selenium
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            BtnProceedToCheckout.Click();

            return new ShippingAddressPage(driver);
        }

        public ShoppingCartPage RemoveItemFromShoppingCart(int index)
        {
            Thread.Sleep(2000);
            IWebElement removeButton = productsList[index].FindElement(By.XPath("//a[@class='cart_quantity_delete']"));
            removeButton.Click();

            Thread.Sleep(2000);



            return new ShoppingCartPage(driver);

        }
        public int getNumberOfItemsInShoppingCart()
        {
            return productsList.Count;
        }
    }
}
