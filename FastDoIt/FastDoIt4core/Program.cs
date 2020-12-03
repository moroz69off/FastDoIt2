using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace FastDoIt4core
{
    class Program
    {
        private static readonly string checkoutPath = "ht"+"tp"+"s:"+"//"+"ki"+"th"+".c"+"om"+
            "/pages/international"+"-"+"checkout#Global-e_International_Checkout";

        private static readonly IClock clock = new SystemClock();

        public static bool isDebug { get; private set; } = false;

        public static List<string> ProfileInfoList { get; private set; }

        public static Func<string> FasFunc { get; set; }

        static IWebDriver webDriver;

        static int timeout = 3500, interval = 777; // default

        private static string str;

        private static int profileNum;

        static void Main(string[] args)
        {
            Console.Title = "FastDoIt";
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
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
                            interval = int.Parse(args[i + 1]);
                            break;
                        case "-P":
                            Console.WriteLine($"Profile info: \"{args[i + 1]}\"");
                            string[] profiles = System.IO.File.ReadAllLines("profiles.csv");
                            profileNum = int.Parse(args[i + 1]);
                            ProfileInfoList = new List<string>(profiles[profileNum + 1].Trim(new char['"']).Split(new char[',']));
                            break;
                        default:
                            Console.WriteLine($"Parameter №{i} is not recognized");
                            break;
                    }
                }
            } // get args values

            if (!isDebug)
                ProfileInfoList = GetProfile("profiles.csv", profileNum); // 1 - fisrt profile in profiles list (for multiprofiles work)

            webDriver = new ChromeDriver();

            _ = FastDoItTask();
        }

        private static List<string> GetProfile(string path, int num)
        {
            throw new NotImplementedException();
        }

        private static async System.Threading.Tasks.Task FastDoItTask()
        {
            object ob = null;
            AsyncCallback call = null;
            FasFunc.BeginInvoke(call, ob);
            str = await WriteLog("fast.log", "driver stared " +
                DateTime.Now.ToString() +
                " | driver clock " +
                clock.Now +
                " | process " +
                webDriver.WindowHandles + webDriver.Manage().Logs
                );
        }

        private static Task<string> WriteLog(string path, string message)
        {
            Console.WriteLine(message);
            System.IO.File.AppendAllTextAsync(path, message);
            return new Task<string>(FasFunc);
        }
    }
}
