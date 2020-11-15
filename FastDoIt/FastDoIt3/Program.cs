using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FastDoIt3
{
    class Program
    {
        private static readonly string checkoutPath = "https://kith.com/pages/international-checkout#Global-e_International_Checkout";
        private static readonly IClock clock = new SystemClock();

        public static string sessionId { get; private set; }
        public static int processId { get; private set; }

        static List<string> profileInfoList;

        [Obsolete]
        static void Main(string[] args)
        {
            Console.Title = "FastDoIt";

            List<string> profileInfoList = GetProfile("profiles.csv", 1); // 1 - fisrt profile in profiles list (for multiprofiles work)
            List<string> links = GetLinks();

            ChromeDriverService chromeDriverService = AddService();

            ChromeOptions options = InitOptions();

            IWebDriver driver = new ChromeDriver(chromeDriverService, options);
            
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(links[0]);

            #region // check that the server has given the good page
            bool isAgoodResponse = IsGoodResponse(links[0]);
            bool IsGoodResponse(string link) { return true; }

            WebDriverWait wait = new WebDriverWait(clock, driver,
                TimeSpan.FromSeconds(180),
                TimeSpan.FromMilliseconds(777)
                );

            try
            {
                isAgoodResponse = wait.Until(d => GetStatusCode(driver));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            #endregion // check that the server has given the good page

            if (isAgoodResponse)
            {
                try
                {
                    driver.FindElement(By.ClassName("goAccept")).Click();
                    System.Threading.Thread.Sleep(777);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                }// accept cookie

                #region automation
                //backToShop
                try
                {
                    driver.FindElement(By.ClassName("backToShop")).Click();
                    System.Threading.Thread.Sleep(777);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                } // backToShop btn

                try
                {
                    ReadOnlyCollection<IWebElement> webElements = driver.FindElements(By.ClassName("swatch-element"));
                    for (int i = 0; i < webElements.Count; i++)
                    {
                        if (webElements[i].Text==profileInfoList[0])
                        {
                            webElements[i].Click();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                } // size select

                try
                {
                    driver.FindElement(By.ClassName("product-form__add-to-cart")).Click();
                    System.Threading.Thread.Sleep(777);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                }//add-to-cart btn

                do
                {
                    try
                    {
                        var cartContainer = driver.FindElement(By.Id("CartContainer"));
                        var btns = cartContainer.FindElements(By.TagName("button"));
                        for (int i = 0; i < btns.Count; i++)
                        {
                            if (btns[i].Text == "CHECKOUT")
                            {
                                btns[i].Click();
                                System.Threading.Thread.Sleep(4444);
                                DoPay(driver);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                } while (driver.Url != checkoutPath);
                #endregion automation
            }

            driver.Quit();
            Console.ReadLine();
        }

        private static bool GetStatusCode(IWebDriver driver)
        {
            if (!(driver.Title == "404 Not Found – Kith")) return true;
            else return false;
        }

        private static void DoPay(IWebDriver driver)
        {
            Console.WriteLine("Fill form...");
            //ЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖЖ
            WebDriverWait wait = new WebDriverWait(clock, driver,
                TimeSpan.FromSeconds(5),
                TimeSpan.FromMilliseconds(100)
                );
            var durl = driver.Url;
            try
            {
                IWebElement element0 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingFirstName")));
                element0.SendKeys(profileInfoList[1]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element1 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingLastName")));
                element1.SendKeys(profileInfoList[2]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element2 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.Email")));
                element2.SendKeys(profileInfoList[3]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element3 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingAddress1")));
                element3.SendKeys(profileInfoList[4]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[2] / div / input first name
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[3] / div / input last name
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[4] / div / input email
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[6] / div / input address1
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[8] / div / input sity
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[10] / div / input zip code
//                  / html / body / div[2] / div[2] / form[1] / div[1] / div[1] / div / div[2] / div[12] / div / input mobile phone

            //IWebElement element4 = driver.FindElement(By.Name("CheckoutData.BillingCity"));
            //    element4.SendKeys(profileInfoList[2]);

            //IWebElement element5 = driver.FindElement(By.Name("CheckoutData.BillingZIP"));
            //    element5.SendKeys(profileInfoList[2]);

            //IWebElement element6 = driver.FindElement(By.Name("CheckoutData.BillingPhone"));
            //    element6.SendKeys(profileInfoList[2]);
        }

        private static List<string> GetLinks()
        {
            return new List<string>(File.ReadAllLines("links").ToList());
        }

        private static ChromeOptions InitOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference("Browser", LogLevel.All); // temp
            options.SetLoggingPreference("Driver", LogLevel.All);  // temp
            //options.AddArguments("--disable-infobars"); // temp
            //object preference = null;
            //options.AddLocalStatePreference("", preference);
            return options;
        }

        /// <summary>
        /// Get profile information from "profiles.csv" file by index
        /// </summary>
        /// <param name="profilesPath">string, path to "profiles.csv" file</param>
        /// <param name="profileNum">index of profile</param>
        /// <returns>List<string> profile data</returns>
        private static List<string> GetProfile(string profilesPath, int profileNum)
        {
            List<List<string>> profilesData = new List<List<string>>();
            List<string> vs = new List<string>(File.ReadAllLines(profilesPath));
            for (int i = 0; i < vs.Count; i++)
            {
                profilesData.Add(vs[i].Split(new char[] { ',' }).ToList());
            }
            return new List<string>(profilesData[profileNum]);
        }

        private static ChromeDriverService AddService()
        {
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = false;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            return chromeDriverService;
        }
    }
}
