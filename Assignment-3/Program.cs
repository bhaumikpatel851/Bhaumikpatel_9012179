using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

class FlipkartAutomation
{
    static void Main(string[] args)
    {
        // Replace with your Flipkart credentials
        string username = "your_email_or_phone";
        string password = "your_password";

        // Replace with your card details
        string cardNumber = "your_card_number";
        string expiryDate = "your_expiry_date";
        string cvv = "your_cvv";
        string cardHolderName = "your_name_on_card";

        // Setting up the Chrome driver
        IWebDriver driver = new ChromeDriver();
        Thread.Sleep(2000);

        // Navigating to Flipkart home page
        driver.Navigate().GoToUrl("https://www.flipkart.com/");
        Thread.Sleep(5000);

        // Closing the login popup if it appears
        try
        {
            IWebElement closeLoginPopup = driver.FindElement(By.XPath("//button[contains(text(), '✕')]"));
            closeLoginPopup.Click();
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("No login popup to close.");
        }

        // Logging into Flipkart
        try
        {
            IWebElement loginLink = driver.FindElement(By.XPath("//a[contains(text(), 'Login')]"));
            loginLink.Click();
            Thread.Sleep(2000);

            IWebElement emailField = driver.FindElement(By.XPath("//input[@class='_2IX_2- VJZDxU']"));
            IWebElement passwordField = driver.FindElement(By.XPath("//input[@type='password']"));
            IWebElement loginButton = driver.FindElement(By.XPath("(//button[@type='submit'])[2]"));

            emailField.SendKeys(username);
            passwordField.SendKeys(password);
            loginButton.Click();
            Thread.Sleep(5000);
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Login elements not found.");
        }

        // Searching for T-shirt
        IWebElement searchBox = driver.FindElement(By.Name("q"));
        searchBox.SendKeys("T-shirt");
        searchBox.SendKeys(Keys.Enter);
        Thread.Sleep(5000);

        // Selecting the first product from the search results
        try
        {
            IWebElement firstProduct = driver.FindElement(By.XPath("//*[@id=\"container\"]/div/div[3]/div[1]/div[2]/div[2]/div/div[2]/div"));
            ScrollToElement(driver, firstProduct);
            firstProduct.Click();
            Thread.Sleep(5000);

            // Switching to the new tab
            string originalWindow = driver.CurrentWindowHandle;
            foreach (string window in driver.WindowHandles)
            {
                if (window != originalWindow)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            Thread.Sleep(2000);

            // Clicking on "Add to Cart" button
            try
            {
                IWebElement addToCartButton = driver.FindElement(By.XPath("//*[@id=\"container\"]/div/div[3]/div[1]/div[1]/div[2]/div/ul/li[1]/button"));
                ScrollToElement(driver, addToCartButton);
                addToCartButton.Click();
                Thread.Sleep(5000);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Add to Cart button not found.");
            }

            // Clicking on "Place Order" button
            try
            {
                IWebElement placeOrderButton = driver.FindElement(By.XPath("//*[@id=\"container\"]/div/div[2]/div/div/div[1]/div/div[3]/div/form/button/span"));
                ScrollToElement(driver, placeOrderButton);
                placeOrderButton.Click();
                Thread.Sleep(5000);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Place Order button not found.");
            }

            // Selecting delivery address
            try
            {
                IWebElement deliverHereButton = driver.FindElement(By.XPath("//*[@id=\"CNTCT0C3F100F5AED43769C0D7EC6C\"]/button"));
                ScrollToElement(driver, deliverHereButton);
                deliverHereButton.Click();
                Thread.Sleep(5000);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Deliver Here button not found.");
            }

            // Choosing payment method
            try
            {
                // Choose a payment method (e.g., Credit/Debit Card, Net Banking, etc.)
                // This example assumes selecting Credit/Debit Card option
                IWebElement paymentMethod = driver.FindElement(By.XPath("//*[@id=\"container\"]/div/div[2]/div/div[1]/div[4]/h3/span[2]"));
                ScrollToElement(driver, paymentMethod);
                paymentMethod.Click();
                Thread.Sleep(5000);

                // Entering card details
                IWebElement cardNumberField = driver.FindElement(By.XPath("//input[@name='cardNumber']"));
                IWebElement expiryDateField = driver.FindElement(By.XPath("//input[@name='expiryDate']"));
                IWebElement cvvField = driver.FindElement(By.XPath("//input[@name='cvv']"));
                IWebElement cardHolderNameField = driver.FindElement(By.XPath("//input[@name='nameOnCard']"));

                cardNumberField.SendKeys(cardNumber);
                expiryDateField.SendKeys(expiryDate);
                cvvField.SendKeys(cvv);
                cardHolderNameField.SendKeys(cardHolderName);
                Thread.Sleep(5000);

                // Clicking on "Pay Now" button
                IWebElement payNowButton = driver.FindElement(By.XPath("//*[@id=\"container\"]/div/div[2]/div/div[1]/div[4]/h3/span[2]"));
                ScrollToElement(driver, payNowButton);
                payNowButton.Click();
                Thread.Sleep(10000); // Wait for the payment process to complete
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Payment method elements not found.");
            }

            // Switching back to the original window
            driver.SwitchTo().Window(originalWindow);
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("First product not found.");
        }

        // Closing the webdriver
        driver.Quit();
    }

    // Helper method to scroll to an element
    private static void ScrollToElement(IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
    }
}
