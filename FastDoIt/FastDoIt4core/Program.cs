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
                            ProfileInfoList = GetProfile("profiles.csv", profileNum);
                            break;
                        default:
                            Console.WriteLine($"Parameter №{i} is not recognized");
                            break;
                    }
                }
            } // get args values

            webDriver = new ChromeDriver();
        }

        private static List<string> GetProfile(string path, int num)
        {
            string str = System.IO.File.ReadAllLines(path)[num];
            return new List<string>(str.Trim(new char['"']).Split(new char[',']));
        }
    }
}
