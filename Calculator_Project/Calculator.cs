using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project
{
    static class Calculator
    {

        internal static void InputData()
        {
            Console.WriteLine();
            Console.WriteLine("To stop adding data input 0 or negative value for X.");
            CalculatorDAO.ClearInputOnly();
            string tmp;
            double x = 1;
            double y;
            while (x > 0)
            {
                Console.Write("Input X value: ");
                tmp = Console.ReadLine();
                if (Menu.IsDouble(tmp))
                {
                    x = Convert.ToDouble(tmp);
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try Again.");
                    continue;
                }
                if (x <= 0)
                {
                    continue;
                }

                Console.Write("Input Y value: ");
                tmp = Console.ReadLine();
                if (Menu.IsDouble(tmp))
                {
                    y = Convert.ToDouble(tmp);
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try Again.");
                    x = 1;
                    continue;
                }
                if(y == 0)
                {
                    Console.WriteLine("Y cannot be equal to 0, set not implemented");
                    continue;
                }
                CalculatorDAO.AddInputLine(x,y);
            }
            Console.WriteLine();
        }

        internal static void CrossJoinData()
        {
            CalculatorDAO.ClearResultsOnly();
            CalculatorDAO.CalculateLines();
            Console.WriteLine();
            Console.WriteLine("Calculations Complete");
            Console.WriteLine();
        }

        internal static void PrintCalc()
        {
            CalculatorDAO.PrintCalc();
            Console.WriteLine();
        }

        internal static void PrintResults()
        {
            double[] results;
            CalculatorDAO.RetrieveResults(out results);
            Console.WriteLine("Results");
            for (int index = 0; index < results.Length; index++)
            {
                Console.WriteLine(results[index]);
            }
            Console.WriteLine();
        }

        internal static void ClearDB()
        {
            CalculatorDAO.ClearDB();
            Console.WriteLine("All data cleared\n");
            Console.WriteLine();
        }
    }
}
