using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            int action = 0;
            while (action != 6)
            {
                action = Menu.StartMenu();
                switch (action)
                {
                    case 1:
                        Calculator.InputData();
                        break;
                    case 2:
                        Calculator.CrossJoinData();
                        break;
                    case 3:
                        Calculator.PrintCalc();
                        break;
                    case 4:
                        Calculator.PrintResults();
                        break;
                    case 5:
                        Calculator.ClearDB();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
