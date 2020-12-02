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

        public static string switchOn { get; private set; } = "";

        public static List<string> ProfileInfoList { get; private set; }

        static int timeout = 3500, interval = 777; // default

        static void Main(string[] args)
        {

            Console.ReadLine();
        }
    }
}
