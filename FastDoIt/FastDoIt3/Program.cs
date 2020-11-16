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

        public static string SessionId { get; private set; }
        public static int ProcessId { get; private set; }

        static List<string> profileInfoList;
        static int timeout = 180, interval = 777;

        [Obsolete]
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                timeout = int.Parse(args[0]);
                interval = int.Parse(args[1]);
                //switch (args[1])
                //{
                //    case "-TOUT":
                //        // ...
                //        break;
                //    case "-SPAN":
                //        // ...
                //        break;
                //    default:
                //        // ...
                //        break;
                //}
            }
            Console.Title = "FastDoIt";

            profileInfoList = GetProfile("profiles.csv", 1); // 1 - fisrt profile in profiles list (for multiprofiles work)
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
                TimeSpan.FromSeconds(timeout),
                TimeSpan.FromMilliseconds(interval)
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
                catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // size select

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
                        var cardContainer = driver.FindElement(By.Id("CartContainer"));
                        var btns = cardContainer.FindElements(By.TagName("button"));
                        for (int i = 0; i < btns.Count; i++)
                        {
                            if (btns[i].Text == "CHECKOUT")
                            {
                                btns[i].Click();
                                System.Threading.Thread.Sleep(3333);
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

        /// <summary>
        /// fills in the fields of the payment form for the card and delivery address
        /// </summary>
        /// <param name="driver">IWebDriver driver</param>
        private static void DoPay(IWebDriver driver)
        {
            Console.WriteLine("Fill form...");

            WebDriverWait wait = new WebDriverWait(clock, driver,
                TimeSpan.FromSeconds(5),
                TimeSpan.FromMilliseconds(100)
                );
            //go to the address form iframe
            #region address form iframe
            var durl = driver.Url;//temp
            var iframeFormAddressDriver = wait.Until(d => driver.SwitchTo().Frame("Intrnl_CO_Container"));

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

            try
            {
                IWebElement element4 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingCity")));
                element4.SendKeys(profileInfoList[5]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element5 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingZIP")));
                element5.SendKeys(profileInfoList[6]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element6 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingPhone")));
                element6.SendKeys(profileInfoList[7]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }
            #endregion

            //go to the card form iframe
            #region card form iframe
            var cardFormIframe = wait.Until(d => driver.SwitchTo().Frame(driver.FindElement(By.Id("secureWindow"))));

            try //PaymentData.cardNum
            {
                IWebElement element = wait.Until(d => driver.FindElement(By.Name("PaymentData.cardNum")));
                element.SendKeys(profileInfoList[8].Replace(" ", ""));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // PaymentData.cardNum

            try
            {
                string month = profileInfoList[9].Remove(2);
                var buttonka = wait.Until(d => driver.FindElement(By.XPath($"/html/body/form/div/div/div[2]/div/div/div[1]/div/select/option[{GetMonth(month)}]")));
                buttonka.Click();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // month

            try // year
            {
                string year = profileInfoList[9].Remove(0, 3);
                driver.FindElement(
                    By.XPath(
                        $"/html/body/form/div/div/div[2]/div/div/div[2]/div/select/option[{GetYear(year)}]")).Click();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // year

            try // PaymentData.cvdNumber
            {
                IWebElement element = driver.FindElement(By.Name("PaymentData.cvdNumber"));
                element.SendKeys(profileInfoList[10]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // PaymentData.cvdNumber

            try // PAY AND PLACE ORDER
            {
                driver.SwitchTo().ParentFrame();
                var btns = driver.FindElements(By.TagName("button"));
                for (int i = 0; i < btns.Count; i++)
                {
                    if (btns[i].Text == "PAY AND PLACE ORDER")
                    {
                        Console.WriteLine("Button kak-by clicked");
                        Console.WriteLine(timeout);
                        Console.WriteLine(interval);
                        //btns[i].Click();
                        return;
                    }
                }
                Console.WriteLine("button Id(\"btnPay\") cliked");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // PAY AND PLACE ORDER
            #endregion
        }

        private static string GetMonth(string month)
        {
            int i = int.Parse(month);
            return (++i).ToString("00");
        }

        private static string GetYear(string year)
        {
            int i = int.Parse(year);
            int indexOption = i - 2018;
            return (indexOption).ToString();
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
