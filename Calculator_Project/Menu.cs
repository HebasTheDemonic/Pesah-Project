using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project
{
    static class Menu 
    {
        // contains User interface and input checks
        static Menu()
        {

        }

        internal static int StartMenu()
        {
            int action;
            string tmp;

            Console.WriteLine("Welcome to Cross Join Calculator!");
            Console.WriteLine("Please Choose an Option:");
            Console.WriteLine("1. Input new data set");
            Console.WriteLine("2. Cross Join current data set");
            Console.WriteLine("3. Print full Calculations");
            Console.WriteLine("4. Print results only");
            Console.WriteLine("5. Clear all Calculations");
            Console.WriteLine("6. Exit");
            Console.Write("Action: ");

            tmp = Console.ReadLine();

            if (IsDouble(tmp))
            {
                if (IsInt32(tmp))
                {
                    action = Convert.ToInt32(tmp);
                }
                else
                {
                    Console.WriteLine("Error! no corresponding option found!");
                    Console.WriteLine();
                    return StartMenu();
                }
            }
            else
            {
                Console.WriteLine("Error! Input was not a number!");
                Console.WriteLine();
                return StartMenu();
            }

            if (action > 0 && action < 7)
            {
                return action;
            }
            else
            {
                Console.WriteLine("Error! no corresponding option found!");
                Console.WriteLine();
                return StartMenu();
            }
        }

        internal static bool IsDouble(string text)
        {
            Double num;
            bool isDouble = false;

            
            if (string.IsNullOrEmpty(text))
            {
                return isDouble;
            }

            isDouble = Double.TryParse(text, out num);

            return isDouble;
        }

        internal static bool IsInt32(string text)
        {
            Int32 num;
            bool isDouble = false;


            if (string.IsNullOrEmpty(text))
            {
                return isDouble;
            }

            isDouble = Int32.TryParse(text, out num);

            return isDouble;
        }
    }
}
