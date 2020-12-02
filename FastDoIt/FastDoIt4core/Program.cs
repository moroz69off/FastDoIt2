using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace FastDoIt4core
{
    class Program
    {
        private static readonly string checkoutPath = "https://kith.com"+
            "/pages/international-checkout#Global-e_International_Checkout";

        private static readonly IClock clock = new SystemClock();

        public static bool isDebug { get; private set; } = false;

        public static List<string> ProfileInfoList { get; private set; }

        static int timeout = 3500, interval = 777; // default

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
                            ProfileInfoList = new List<string>(args[i + 1].Trim(new char['"']).Split(new char[',']));
                            break;
                        default:
                            Console.WriteLine($"Parameter №{i} is not recognized");
                            break;
                    }
                }
            } // get args values


        }
    }
}
