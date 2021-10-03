using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_7072_0771
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7072();

            Welcome0771();
            Console.ReadKey();
        }

        static partial void Welcome0771();
        private static void Welcome7072()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to myfirst console application", name);
        }
    }
}


