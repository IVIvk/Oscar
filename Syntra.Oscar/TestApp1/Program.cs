using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int number1 = 0;
            int number2 = 0;
            int result = 0;
            // Reviewed in VS

            for (int i = 0; i < 2; i++)
            {
                Console.Write("Please enter a number: ");
                if (i == 0)
                {
                    number1 = int.Parse(Console.ReadLine());
                }

                if (i == 1)
                {
                    number2 = int.Parse(Console.ReadLine());
                }
            }

            result = number1 + number2;
            Console.WriteLine($"Number 1 ({number1}) + number 2 ({number2}) = {result}");
            Console.ReadKey();


        }
    }
}
