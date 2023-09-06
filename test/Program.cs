using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "İ";
            test.ToLower();
            Console.WriteLine(test.ToLower());
        }
    }
}
