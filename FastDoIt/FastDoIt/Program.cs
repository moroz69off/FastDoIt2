using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FastDoIt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ResourceWriter RW = new ResourceWriter(@".\CarResources.resources"))
            {
                RW.AddResource("isActionGoodResult", false);
            }
            Actor actor = new Actor(args[0]);
                Console.ReadLine();
        }
        class Actor
        {
            public Actor()
            {
                if (true)
                {
                    isGoodResult = true;
                }
            }

            public Actor(string result)
            {
                if (result=="o_0")
                {
                    isGoodResult = true;
                }
            }
            bool isGoodResult { get; set; } = false;
        }
    }
}
