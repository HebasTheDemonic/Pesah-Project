using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To The Web Store.");
            int menuOption = 0;
            while (menuOption != 6)
            {
                menuOption = Menu.MainMenu();
            }
        }
    }
}

