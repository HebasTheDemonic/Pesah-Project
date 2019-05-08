using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    static class Menu
    {
        static public void MainMenu()
        {
            int choice;
            Console.WriteLine();
            Console.WriteLine("Please Choose an Option from the Menu:");
            Console.WriteLine("1. Existing User Login.");
            Console.WriteLine("2. New User Registration.");
            Console.WriteLine("3. Existing Supplier Login.");
            Console.WriteLine("4. New Supplier Login.");
            Console.WriteLine("5. Exit");
            Console.Write("Option number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    UserLogin();
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    return;
                default:
                    throw new OptionDoesNotExistException();
            }
        }

        static void UserLogin()
        {
            Console.Write("Input Username: ");
            string username = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input Password: ");
            string password = Console.ReadLine();
            Console.WriteLine();

            Customer customer = new Customer(username, password);
            while (customer.CustomerID == 0)
            {
                try
                {
                    OrderManagmentDAO.CustomerLogin(customer);
                }
                catch (UserDoesNotExistException)
                {
                    Console.WriteLine("This user does not exist.");
                    Console.WriteLine("Input correct username or type // to exit.");
                    username = Console.ReadLine();
                    if (username == "//")
                    {
                        return;
                    }
                    else
                    {
                        customer.Username = username;
                        continue;
                    }
                }
                catch (WrongPasswordException)
                {
                    Console.WriteLine("Incorrect password.");
                    Console.WriteLine("Input correct password or type // to exit.");
                    password = Console.ReadLine();
                    if (password == "//")
                    {
                        return;
                    }
                    else
                    {
                        customer.Password = password;
                        continue;
                    }
                }
                finally
                {
                    ExistingUserMenu(customer);
                }
            }

        }

        static void ExistingUserMenu(Customer customer)
        {

        }
    }
}
