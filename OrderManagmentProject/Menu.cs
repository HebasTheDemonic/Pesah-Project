using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagmentProject
{
    static class Menu
    {
        static public int MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please Choose an Option from the Menu:");
            Console.WriteLine("1. Existing User Login.");
            Console.WriteLine("2. New User Registration.");
            Console.WriteLine("3. Existing Supplier Login.");
            Console.WriteLine("4. New Supplier Login.");
            Console.WriteLine("5. Print Registered Actions");
            Console.WriteLine("6. Exit");
            Console.Write("Option number: ");
            try
            {
                string tmp = Console.ReadLine();

                int choice = CheckChoice(6, tmp);
                Console.WriteLine();
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", $"Option {choice} selected");
                switch (choice)
                {
                    case 1:
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", "Customer Login Started");
                        UserLogin();
                        return MainMenu();
                    case 2:
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", "New Customer Registration Started");
                        AddNewCustomer();
                        return MainMenu();
                    case 3:
                        OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", "Supplier Login Started");
                        SupplierLogin();
                        return MainMenu();
                    case 4:
                        AddNewSupplier();
                        OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", "New Supplier Registration Started");
                        return MainMenu();
                    case 5:
                        PrintRegistry();
                        return MainMenu();
                    case 6:
                        OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Program Shut Down");
                        return 6;
                }
            }
            catch (InputIsOutOfBoundsException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException", "Exception Handeled");
                Console.WriteLine("Input must be between 1-6.");
                Console.WriteLine();
                MainMenu();
                return 1;
            }
            catch (InputIsNotAnIntegerException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException", "Exception Handeled");
                Console.WriteLine("Input must be an Integer between 1-6.");
                Console.WriteLine();
                MainMenu();
                return 1;
            }
            catch (MustInputANumberException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException", "Exception Handeled");
                Console.WriteLine("Input must be an Integer between 1-6.");
                MainMenu();
                return 1;
            }
            return 1;
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
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Login Attempt", "Login Succesful");
                    ExistingCustomerMenu(customer);

                }
                catch (UserDoesNotExistException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Login Attempt", "Login Failed");
                    OrderManagmentDAO.ActionRegistry("System", "UserDoesNotExistException");
                    Console.WriteLine("This user does not exist.");
                    Console.WriteLine("Input correct username or type // to exit.");
                    username = Console.ReadLine();
                    Console.WriteLine();
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
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Login Attempt", "Login Failed");
                    OrderManagmentDAO.ActionRegistry("System", "WrongPasswordException");
                    Console.WriteLine("Incorrect password.");
                    Console.WriteLine("Input correct password or type // to exit.");
                    password = Console.ReadLine();
                    Console.WriteLine();
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
            }

        }

        static void SupplierLogin()
        {
            Console.Write("Input Username: ");
            string username = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Input Password: ");
            string password = Console.ReadLine();
            Console.WriteLine();

            Supplier supplier = new Supplier(username, password);

            while (supplier.ID == 0)
            {
                try
                {
                    OrderManagmentDAO.SupplierLogin(supplier);
                    OrderManagmentDAO.ActionRegistry("User - Supplier", "Login Attempt", "Login Succesful");
                    ExistingSupplierMenu(supplier);
                }
                catch (UserDoesNotExistException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Supplier", "Login Attempt", "Login Failed");
                    OrderManagmentDAO.ActionRegistry("System", "SupplierDoesNotExistException");
                    Console.WriteLine("This supplier does not exist.");
                    Console.WriteLine("Input correct username or type // to exit.");
                    username = Console.ReadLine();
                    Console.WriteLine();
                    if (username == "//")
                    {
                        return;
                    }
                    else
                    {
                        supplier.Username = username;
                        continue;
                    }
                }
                catch (WrongPasswordException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Supplier", "Login Attempt", "Login Failed");
                    OrderManagmentDAO.ActionRegistry("System", "WrongPasswordException");
                    Console.WriteLine("Incorrect password.");
                    Console.WriteLine("Input correct password or type // to exit.");
                    password = Console.ReadLine();
                    Console.WriteLine();
                    if (password == "//")
                    {
                        return;
                    }
                    else
                    {
                        supplier.Password = password;
                        continue;
                    }
                }
            }
        }

        static void ExistingCustomerMenu(Customer customer)
        {
            Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}!");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Display a list of user's orders.");
            Console.WriteLine("2. Display list of all available products.");
            Console.WriteLine("3. Make a new order.");
            Console.WriteLine("4. Exit.");
            Console.Write("Option number: ");
            try
            {
                string tmp = Console.ReadLine();
                int choice = CheckChoice(4, tmp);
                Console.WriteLine();
                OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", $"Option {choice} selected");
                switch (choice)
                {
                    case 1:
                        OrderManagmentDAO.PrintCustomerOrders(customer);
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Data Request", "Customer Orders Printed");
                        ExistingCustomerMenu(customer);
                        return;
                    case 2:
                        OrderManagmentDAO.PrintAllProducts();
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Data Request", "All Available Products Printed");
                        ExistingCustomerMenu(customer);
                        return;
                    case 3:
                        AddNewOrder(customer);
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "New Order Made");
                        ExistingCustomerMenu(customer);
                        return;
                    case 4:
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "Logout");
                        return;
                }
            }
            catch (InputIsOutOfBoundsException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException");
                Console.WriteLine("Input must be between 1-4.");
                Console.WriteLine();
                ExistingCustomerMenu(customer);
                return;
            }
            catch (InputIsNotAnIntegerException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException");
                Console.WriteLine("Input must be an Integer between 1-4.");
                Console.WriteLine();
                ExistingCustomerMenu(customer);
                return;
            }
            catch (MustInputANumberException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "MustInputANumberException");
                Console.WriteLine("Input must be a NUMBER between 1-4.");
                Console.WriteLine();
                ExistingCustomerMenu(customer);
                return;
            }
            catch (InsufficientDataException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Data Request", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "InsufficientDataException");
                Console.WriteLine("Corrupted data detected! returning to Main Menu.");
                Console.WriteLine();
                return;
            }
            catch (UserHasNoOrdersException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "UserHasNoOrdersException");
                Console.WriteLine("No orders have been found for this user.");
                Console.WriteLine();
                ExistingCustomerMenu(customer);
                return;
            }
            catch (StoreHasNoStockException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "StoreHasNoStockException");
                Console.WriteLine("We are sorry, currently the store has no items in stock.");
                ExistingCustomerMenu(customer);
                return;
            }
            
        }

        static void ExistingSupplierMenu(Supplier supplier)
        {
            Console.WriteLine($"welcome supplier from {supplier.CompanyName}.");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Add a new product to inventory.");
            Console.WriteLine($"2. Display all products from {supplier.CompanyName}");
            Console.WriteLine("3. Exit.");
            Console.Write("Option number: ");
            try
            {
                string tmp = Console.ReadLine();
                int choice = CheckChoice(3, tmp);
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", $"Option {choice} selected");
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        AddNewProduct(supplier.ID);
                        OrderManagmentDAO.ActionRegistry("User - Supplier", "Action", "New Product Added");
                        ExistingSupplierMenu(supplier);
                        return;
                    case 2:
                        OrderManagmentDAO.PrintSupplierItems(supplier);
                        OrderManagmentDAO.ActionRegistry("User - Supplier", "Data Request", $"Supplier {supplier.Username}'s products printed");
                        ExistingSupplierMenu(supplier);
                        return;
                    case 3:
                        OrderManagmentDAO.ActionRegistry("User - Supplier", "Action", "Logout");
                        return;
                }
            }
            catch (InputIsOutOfBoundsException)
            {
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException");
                Console.WriteLine("Input must be between 1-3.");
                Console.WriteLine();
                ExistingSupplierMenu(supplier);
                return;
            }
            catch (InputIsNotAnIntegerException)
            {
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException");
                Console.WriteLine("Input must be an Integer between 1-3.");
                Console.WriteLine();
                ExistingSupplierMenu(supplier);
                return;
            }
            catch (MustInputANumberException)
            {
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "MustInputANumberException");
                Console.WriteLine("Input must be a NUMBER between 1-3.");
                Console.WriteLine();
                ExistingSupplierMenu(supplier);
                return;
            }
        }

        static void AddNewOrder(Customer customer)
        {
            string tmp;
            int productID = 0;
            int amount = 0;
            bool flag=true;
            while (flag)
            {
                try
                {
                    Console.Write("Please input the product's ID: ");
                    tmp = Console.ReadLine();
                    Console.WriteLine();
                    productID = CheckInput(tmp);
                }
                catch (InputIsOutOfBoundsException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException");
                    Console.WriteLine("Input must be higher then 0.");
                    Console.WriteLine();
                    continue;
                }
                catch (InputIsNotAnIntegerException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException");
                    Console.WriteLine("Input must be an Integer.");
                    Console.WriteLine();
                    continue;
                }
                catch (MustInputANumberException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "MustInputANumberException");
                    Console.WriteLine("Input must be an Integer.");
                    Console.WriteLine();
                    continue;
                }
                flag = false;
            }
            flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Please input the amount you wish to buy: ");
                    tmp = Console.ReadLine();
                    Console.WriteLine();
                    amount = CheckInput(tmp);
                }
                catch (InputIsOutOfBoundsException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException");
                    Console.WriteLine("Input must be higher then 0.");
                    Console.WriteLine();
                    continue;
                }
                catch (InputIsNotAnIntegerException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException");
                    Console.WriteLine("Input must be an Integer.");
                    Console.WriteLine();
                    continue;
                }
                catch (MustInputANumberException)
                {
                    OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                    OrderManagmentDAO.ActionRegistry("System", "MustInputANumberException");
                    Console.WriteLine("Input must be an Integer.");
                    Console.WriteLine();
                    continue;
                }
                flag = false;
            }
            try
            {
                OrderManagmentDAO.AddNewOrder(customer.CustomerID, productID, amount);
                Console.WriteLine("Order created.");
            }
            catch (InsufficientDataException)
            {
                throw;
            }
            catch (ProductDoesNotExistException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "ProductDoesNotExistException");
                Console.WriteLine("Product ID not recognized.");
                Console.WriteLine();
                return;
            }
            catch (InsufficientInventoryException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Action", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "InsufficientInventoryException");
                Console.WriteLine("Ordered amount is larger than current inventory.");
                Console.WriteLine("Order not created.");
                Console.WriteLine();
                return;
            }

        }

        static void AddNewCustomer(string username = null, string password = null, string firstName = null, string lastName = null, int creditNumber = 0)
        {
            if (username == null)
            {
                Console.Write("Please input desired username: ");
                username = Console.ReadLine();
                Console.WriteLine();
            }

            if (password == null)
            {
                Console.Write("Please input desired password: ");
                password = Console.ReadLine();
                Console.WriteLine();
            }

            if (firstName == null)
            {
                Console.Write("Please input your first name: ");
                firstName = Console.ReadLine();
                Console.WriteLine();
            }

            if (lastName == null)
            {
                Console.Write("Please input your last name: ");
                lastName = Console.ReadLine();
                Console.WriteLine();
            }

            if (creditNumber == 0)
            {
                bool flag = true;
                string tmp;
                while (flag)
                {
                    try
                    {
                        Console.Write("Please input your credit card number: ");
                        tmp = Console.ReadLine();
                        Console.WriteLine();
                        creditNumber = CheckInput(tmp);
                    }
                    catch (InputIsOutOfBoundsException)
                    {
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                        OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException");
                        Console.WriteLine("Input must be higher then 0.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (InputIsNotAnIntegerException)
                    {
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                        OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException");
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (MustInputANumberException)
                    {
                        OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Incorrect Input");
                        OrderManagmentDAO.ActionRegistry("System", "MustInputANumberException");
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    flag = false;
                }
            }

            Customer customer = new Customer(username, password, firstName, lastName, creditNumber);

            try
            {
                OrderManagmentDAO.AddNewCustomer(customer);
                Console.WriteLine($"User {username} added to system.");
            }
            catch (UserAlreadyExistsException)
            {
                OrderManagmentDAO.ActionRegistry("User - Customer", "Data Input", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "UserAlreadyExistsException");
                Console.WriteLine("This username already exists.");
                Console.WriteLine();
                AddNewCustomer(password: password, firstName: firstName, lastName: lastName, creditNumber: creditNumber);
            }
        }

        static void AddNewSupplier(string username = null, string password = null, string companyName = null)
        {
            if (username == null)
            {
                Console.Write("Please input desired username: ");
                username = Console.ReadLine();
                Console.WriteLine();
            }

            if (password == null)
            {
                Console.Write("Please input desired password: ");
                password = Console.ReadLine();
                Console.WriteLine();
            }
            if (companyName == null)
            {
                Console.Write("Please input the company name: ");
                companyName = Console.ReadLine();
                Console.WriteLine();
            }

            Supplier supplier = new Supplier(username, password, companyName);

            try
            {
                OrderManagmentDAO.AddNewSupplier(supplier);
                Console.WriteLine($"New supplier from {companyName} added to system");
            }
            catch (UserAlreadyExistsException)
            {
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Data Input", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "SupplierAlreadyExistsException");
                Console.WriteLine("This username already exists.");
                Console.WriteLine();
                AddNewSupplier(password: password, companyName: companyName);
            }
        }

        static void AddNewProduct(int supplierId, string name = null, int price = 0, int inventory = 0)
        {
            if (price == 0)
            {
                bool flag = true;
                string tmp;
                while (flag)
                {
                    try
                    {
                        Console.Write("Please input price of product: ");
                        tmp = Console.ReadLine();
                        Console.WriteLine();
                        price = CheckInput(tmp);
                    }
                    catch (InputIsOutOfBoundsException)
                    {
                        Console.WriteLine("Input must be higher then 0.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (InputIsNotAnIntegerException)
                    {
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (MustInputANumberException)
                    {
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    flag = false;
                }
            }
            if (inventory == 0)
            {
                bool flag = true;
                string tmp;
                while (flag)
                {
                    try
                    {
                        Console.Write("Please input price of product: ");
                        tmp = Console.ReadLine();
                        Console.WriteLine();
                        inventory = CheckInput(tmp);
                    }
                    catch (InputIsOutOfBoundsException)
                    {
                        Console.WriteLine("Input must be higher then 0.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (InputIsNotAnIntegerException)
                    {
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    catch (MustInputANumberException)
                    {
                        Console.WriteLine("Input must be an Integer.");
                        Console.WriteLine();
                        continue;
                    }
                    flag = false;
                }
            }
            if (name == null)
            {
                Console.Write("Please input the product's name: ");
                name = Console.ReadLine();
                Console.WriteLine();
            }

            Product product = new Product(name, supplierId, price, inventory);

            try
            {
                OrderManagmentDAO.AddNewProduct(product);
                Console.WriteLine("Product has been added succesfully.");
                Console.WriteLine();
            }
            catch (ProductAlreadyExistsException)
            {
                OrderManagmentDAO.ActionRegistry("User - Supplier", "Data Input", "Exception");
                OrderManagmentDAO.ActionRegistry("System", "ProductAlreadyExistsException");
                Console.WriteLine("This product already exist under a diffrent supplier.");
                Console.WriteLine();
                return;
            }
        }

        static void PrintRegistry()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("How many rows would you like to display?");
                Console.Write("Input an Integer:");
                string lengthString = Console.ReadLine();
                int length = CheckInput(lengthString);
                Console.WriteLine();
                OrderManagmentDAO.PrintRegistry(length);
            }
            catch (InputIsOutOfBoundsException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsOutOfBoundsException", "Exception Handeled");
                Console.WriteLine("Input must be between 1-6.");
                Console.WriteLine();
                PrintRegistry();
            }
            catch (InputIsNotAnIntegerException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException", "Exception Handeled");
                Console.WriteLine("Input must be an Integer between 1-6.");
                Console.WriteLine();
                PrintRegistry();
            }
            catch (MustInputANumberException)
            {
                OrderManagmentDAO.ActionRegistry("User", "Menu Input", "Incorrect Input");
                OrderManagmentDAO.ActionRegistry("System", "InputIsNotAnIntegerException", "Exception Handeled");
                Console.WriteLine("Input must be an Integer between 1-6.");
                PrintRegistry();
            }

        }

    static public int CheckChoice(int maxOption, string Choice)
        {
            if (IsDouble(Choice))
            {
                if (IsInt32(Choice))
                {
                    int result = Convert.ToInt32(Choice);
                    if (maxOption < result || result <= 0)
                    {
                        throw new InputIsOutOfBoundsException();
                    }
                    return result;
                }
                else
                {
                    throw new InputIsNotAnIntegerException();
                }
            }
            else
            {
                throw new MustInputANumberException();
            }
        }

        static public int CheckInput(string Choice)
        {
            if (IsDouble(Choice))
            {
                if (IsInt32(Choice))
                {
                    int result = Convert.ToInt32(Choice);
                    if (result < 0)
                    {
                        throw new InputIsOutOfBoundsException();
                    }
                    return result;
                }
                else
                {
                    throw new InputIsNotAnIntegerException();
                }
            }
            else
            {
                throw new MustInputANumberException();
            }
        }

        static bool IsDouble(string text)
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

        static bool IsInt32(string text)
        {
            Int32 num;
            bool isInt32 = false;


            if (string.IsNullOrEmpty(text))
            {
                return isInt32;
            }

            isInt32 = Int32.TryParse(text, out num);

            return isInt32;
        }
    }
}
