﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using RES = FastDoIt3.Properties.Resources;

namespace FastDoIt3
{
    class Program
    {
        private static readonly string checkoutPath = RES.checkoutPath;
        private static readonly IClock clock = new SystemClock();

        public static string SessionId { get; private set; }
        public static int ProcessId { get; private set; }
        public static bool isDebug { get; private set; } = false;
        public static string switchOn { get; private set; } = "";

        public static List<string> ProfileInfoList { get; private set; }

        static int timeout = 3500, interval = 777;

        [Obsolete]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switchOn = args[i];
                    switch (switchOn)
                    {
                        case "-D":
                            Console.WriteLine("Debug mode enabled.");
                            isDebug = true;
                            break;
                        case "-T":
                            Console.WriteLine($"Timeout = {int.Parse(args[i + 1])}");
                            timeout = int.Parse(args[++i]);
                            break;
                        case "-I":
                            Console.WriteLine($"Interval = {int.Parse(args[i + 1])}");
                            interval = int.Parse(args[i+1]);
                            break;
                        case "-P":
                            Console.WriteLine($"Profile info: \"{args[i + 1]}\"");
                            ProfileInfoList = new List<string>(args[i + 1].Trim(new char['"']).Split(new char[',']));
                            break;
                        default:
                            Console.WriteLine($"Parameter №{i} is not recognized");
                            break;
                    }
                }
            }
            Console.Title = "FastDoIt";

            if (!args.Contains("-D"))
                ProfileInfoList = GetProfile("profiles.csv", 1); // 1 - fisrt profile in profiles list (for multiprofiles work)
            
            List<string> links = GetLinks();

            ChromeDriverService chromeDriverService = AddService();

            ChromeOptions options = InitOptions();

            IWebDriver driver = new ChromeDriver(chromeDriverService, options);
            
            var wait10s = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

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

                try  // size select
                {
                    ReadOnlyCollection<IWebElement> webElements = driver.FindElements(By.ClassName("swatch-element"));
                    for (int i = 0; i < webElements.Count; i++)
                    {
                        if (webElements[i].Text==ProfileInfoList[0])
                        {
                            webElements[i].Click();
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // size select

                try //add-to-cart btn
                {
                    driver.FindElement(By.ClassName("product-form__add-to-cart")).Click();
                    System.Threading.Thread.Sleep(777);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }//add-to-cart btn

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
                                System.Threading.Thread.Sleep(2222);
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

        /// <summary>
        /// Checks that the response code is not 404
        /// </summary>
        /// <param name="driver">IWebDriver driver</param>
        /// <returns>bool is not 404 status code</returns>
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
                element0.SendKeys(ProfileInfoList[1]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element1 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingLastName")));
                element1.SendKeys(ProfileInfoList[2]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element2 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.Email")));
                element2.SendKeys(ProfileInfoList[3]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element3 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingAddress1")));
                element3.SendKeys(ProfileInfoList[4]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element4 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingCity")));
                element4.SendKeys(ProfileInfoList[5]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element5 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingZIP")));
                element5.SendKeys(ProfileInfoList[6]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }

            try
            {
                IWebElement element6 = wait.Until(d => driver.FindElement(By.Name("CheckoutData.BillingPhone")));
                element6.SendKeys(ProfileInfoList[7]);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); }
            #endregion

            //go to the card form iframe and push pay button
            #region card form iframe
            var cardFormIframe = wait.Until(d => driver.SwitchTo().Frame(driver.FindElement(By.Id("secureWindow"))));

            try //PaymentData.cardNum
            {
                IWebElement element = wait.Until(d => driver.FindElement(By.Name("PaymentData.cardNum")));
                element.SendKeys(ProfileInfoList[8].Replace(" ", ""));
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // PaymentData.cardNum

            try
            {
                string month = ProfileInfoList[9].Remove(2);
                var buttonka = wait.Until(d => driver.FindElement(By.XPath(RES.monthSelectXpath)));
                buttonka.Click();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // month

            try // year
            {
                string year = ProfileInfoList[9].Remove(0, 3);
                driver.FindElement(By.XPath(RES.yearSelectXpath)).Click();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n" + ex.StackTrace); } // year

            try // PaymentData.cvdNumber
            {
                IWebElement element = driver.FindElement(By.Name("PaymentData.cvdNumber"));
                element.SendKeys(ProfileInfoList[10]);
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
                        if (isDebug)
                        {
                            Console.WriteLine("`PAY AND PLACE ORDER` button `kak-by` clicked");
                        }
                        else 
                        {
                            btns[i].Click();
                        }

                        return;
                    }
                }
                Console.WriteLine("button Id \"btnPay\" cliked");
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
