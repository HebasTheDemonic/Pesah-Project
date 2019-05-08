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
    static class OrderManagmentDAO
    {
        static OrderManagmentDAO()
        {

        }

        static bool DoesCustomerExist(Customer customer)
        {
            if (customer.Username == null || customer.Password == null)
            {
                throw new InsufficientDataException();
            }

            int result;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DOES_USER_EXIST";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = customer.Username;
                usernameParameter.ParameterName = "@USERNAME";

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.Output;
                returnParameter.ParameterName = "@RETURN";

                sqlCommand.Parameters.Add(usernameParameter);
                sqlCommand.Parameters.Add(returnParameter);

                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                result = (int)returnParameter.Value;
                connection.Close();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool DoesProductExist(int productID)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DOES_PRODUCT_EXIST";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.Int;
                usernameParameter.SqlValue = productID;
                usernameParameter.ParameterName = "@PRODUCTID";

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.Output;
                returnParameter.ParameterName = "@RETURN";

                sqlCommand.Parameters.Add(usernameParameter);
                sqlCommand.Parameters.Add(returnParameter);

                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                result = (int)returnParameter.Value;
                connection.Close();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool DoesSupplierExist(Supplier supplier)
        {
            if (supplier.Username == null || supplier.Password == null)
            {
                throw new InsufficientDataException();
            }

            int result;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DOES_USER_EXIST";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = supplier.Username;
                usernameParameter.ParameterName = "@USERNAME";

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.Output;
                returnParameter.ParameterName = "@RETURN";

                sqlCommand.Parameters.Add(usernameParameter);
                sqlCommand.Parameters.Add(returnParameter);

                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                result = (int)returnParameter.Value;
                connection.Close();
            }

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int CheckInventory(int productID)
        {
            if (!DoesProductExist(productID))
            {
                throw new InsufficientDataException();
            }

            int inventory;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "CHECK_INVENTORY";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.Int;
                usernameParameter.SqlValue = productID;
                usernameParameter.ParameterName = "@PRODUCT_ID";

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.Output;
                returnParameter.ParameterName = "@RETURN";

                sqlCommand.Parameters.Add(usernameParameter);
                sqlCommand.Parameters.Add(returnParameter);

                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                inventory = (int)returnParameter.Value;
                connection.Close();
            }

            return inventory;
        }

        static public void CustomerLogin (Customer customer)
        {
            if (DoesCustomerExist(customer) == false)
            {
                throw new UserDoesNotExistException();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand FindUserID = new SqlCommand();
                FindUserID.Connection = connection;
                FindUserID.CommandText = "WHAT_IS_USER_ID";
                FindUserID.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = customer.Username;
                usernameParameter.ParameterName = "@username";

                FindUserID.Parameters.Add(usernameParameter);

                connection.Open();
                SqlDataReader sqlDataReader = FindUserID.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    if (customer.Password == (string)sqlDataReader["PASSWORD"])
                    {
                        customer.CustomerID = (int)sqlDataReader["ID"];
                    }
                    else
                    {
                        connection.Close();
                        throw new WrongPasswordException();
                    }
                }
                connection.Close();
            }
        }

        static public void SupplierLogin(Supplier supplier)
        {
            if (!DoesSupplierExist(supplier))
            {
                throw new SupplierDoesNotExistException();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand FindUserID = new SqlCommand();
                FindUserID.Connection = connection;
                FindUserID.CommandText = "WHAT_IS_SUPPLIER_ID";
                FindUserID.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = supplier.Username;
                usernameParameter.ParameterName = "@username";

                FindUserID.Parameters.Add(usernameParameter);

                connection.Open();
                SqlDataReader sqlDataReader = FindUserID.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    if (supplier.Password == (string)sqlDataReader["PASSWORD"])
                    {
                        supplier.ID = (int)sqlDataReader["ID"];
                    }
                    else
                    {
                        connection.Close();
                        throw new WrongPasswordException();
                    }
                }
                connection.Close();
            }
        }

        static public void AddNewCustomer (Customer customer)
        {
            if (DoesCustomerExist(customer))
            {
                throw new UserAlreadyExistsException();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
               
                SqlCommand AddnewUser = new SqlCommand();
                AddnewUser.Connection = connection;
                AddnewUser.CommandText = "ADD_NEW_CUSTOMER";
                AddnewUser.CommandType = CommandType.StoredProcedure;

                SqlParameter username = new SqlParameter();
                username.SqlDbType = SqlDbType.VarChar;
                username.Value = customer.Username;
                username.ParameterName = "@USERNAME";
                AddnewUser.Parameters.Add(username);

                SqlParameter password = new SqlParameter();
                password.SqlDbType = SqlDbType.VarChar;
                password.Value = customer.Password;
                password.ParameterName = "@PASSWORD";
                AddnewUser.Parameters.Add(password);

                SqlParameter firstName = new SqlParameter();
                firstName.SqlDbType = SqlDbType.VarChar;
                firstName.Value = customer.FirstName;
                firstName.ParameterName = "@FIRSTNAME";
                AddnewUser.Parameters.Add(firstName);

                SqlParameter lastName = new SqlParameter();
                lastName.SqlDbType = SqlDbType.VarChar;
                lastName.Value = customer.LastName;
                lastName.ParameterName = "@LASTNAME";
                AddnewUser.Parameters.Add(lastName);

                SqlParameter creditNumber = new SqlParameter();
                creditNumber.SqlDbType = SqlDbType.Int;
                creditNumber.Value = customer.CreditNumber;
                creditNumber.ParameterName = "@CREDITNUMBER";
                AddnewUser.Parameters.Add(creditNumber);

                connection.Open();
                AddnewUser.ExecuteNonQuery();
                connection.Close();
            }

        }

        static public void AddNewSupplier(Supplier supplier)
        {
            if (DoesSupplierExist(supplier))
            {
                throw new SupplierAlreadyExistsException();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {

                SqlCommand addNewSupplier = new SqlCommand();
                addNewSupplier.Connection = connection;
                addNewSupplier.CommandText = "ADD_NEW_SUPPLIER";
                addNewSupplier.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.Value = supplier.Username;
                usernameParameter.ParameterName = "@USERNAME";
                addNewSupplier.Parameters.Add(usernameParameter);

                SqlParameter passwordParameter = new SqlParameter();
                passwordParameter.SqlDbType = SqlDbType.VarChar;
                passwordParameter.Value = supplier.Password;
                passwordParameter.ParameterName = "@PASSWORD";
                addNewSupplier.Parameters.Add(passwordParameter);

                SqlParameter companyParameter = new SqlParameter();
                companyParameter.SqlDbType = SqlDbType.VarChar;
                companyParameter.Value = supplier.CompanyName;
                companyParameter.ParameterName = "@COMPANYNAME";
                addNewSupplier.Parameters.Add(companyParameter);

                connection.Open();
                addNewSupplier.ExecuteNonQuery();
                connection.Close();
            }

        }

        static public void AddNewProduct(Product product)
        {
            if (DoesProductExist(product.ID))
            {
                throw new ProductAlreadyExistsException();
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {

                SqlCommand AddnewProduct = new SqlCommand();
                AddnewProduct.Connection = connection;
                AddnewProduct.CommandText = "ADD_NEW_CUSTOMER";
                AddnewProduct.CommandType = CommandType.StoredProcedure;

                SqlParameter nameParameter = new SqlParameter();
                nameParameter.SqlDbType = SqlDbType.VarChar;
                nameParameter.Value = product.Name;
                nameParameter.ParameterName = "@NAME";
                AddnewProduct.Parameters.Add(nameParameter);

                SqlParameter idParameter = new SqlParameter();
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Value = product.SupplierID;
                idParameter.ParameterName = "@SUPPLIER_ID";
                AddnewProduct.Parameters.Add(idParameter);

                SqlParameter priceParameter = new SqlParameter();
                priceParameter.SqlDbType = SqlDbType.Int;
                priceParameter.Value = product.Price;
                priceParameter.ParameterName = "@PRICE";
                AddnewProduct.Parameters.Add(priceParameter);

                SqlParameter inventoryParameter = new SqlParameter();
                inventoryParameter.SqlDbType = SqlDbType.Int;
                inventoryParameter.Value = product.Inventory;
                inventoryParameter.ParameterName = "@INVENTORY";
                AddnewProduct.Parameters.Add(inventoryParameter);

                connection.Open();
                AddnewProduct.ExecuteNonQuery();
                connection.Close();
            }
        }

        static public void AddNewOrder(int customerID, int productID, int Amount)
        {
            if (customerID == 0 || productID == 0)
            {
                throw new InsufficientDataException();
            }

            if (Amount <= 0)
            {
                throw new WrongAmountException();
            }

            if (!DoesProductExist(productID))
            {
                throw new ProductDoesNotExistException();
            }

            if (CheckInventory(productID) < Amount)
            {
                throw new InsufficientInventoryException();
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand newOrder = new SqlCommand();
                newOrder.Connection = connection;
                newOrder.CommandText = "MAKE_NEW_ORDER";
                newOrder.CommandType = CommandType.StoredProcedure;

                SqlParameter customerIDParameter = new SqlParameter();
                customerIDParameter.SqlDbType = SqlDbType.Int;
                customerIDParameter.Value = customerID;
                customerIDParameter.ParameterName = "@CUSTOMER_ID";
                newOrder.Parameters.Add(customerIDParameter);

                SqlParameter productIDParameter = new SqlParameter();
                productIDParameter.SqlDbType = SqlDbType.Int;
                productIDParameter.Value = productID;
                productIDParameter.ParameterName = "@PRODUCT_ID";
                newOrder.Parameters.Add(productIDParameter);

                SqlParameter amountParameter = new SqlParameter();
                amountParameter.SqlDbType = SqlDbType.Int;
                amountParameter.Value = Amount;
                amountParameter.ParameterName = "@AMOUNT";
                newOrder.Parameters.Add(amountParameter);

                SqlParameter priceParameter = new SqlParameter();
                priceParameter.SqlDbType = SqlDbType.Int;
                priceParameter.ParameterName = "@PRICE";
                newOrder.Parameters.Add(priceParameter);

                connection.Open();
                newOrder.ExecuteNonQuery();
                connection.Close();
            }
        }

        static public void PrintCustomerOrders (Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                throw new InsufficientDataException();
            }
            string tmp;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand FindOrders = new SqlCommand();
                FindOrders.Connection = sqlConnection;
                FindOrders.CommandText = "SHOW_USER_ORDERS";
                FindOrders.CommandType = CommandType.StoredProcedure;

                SqlParameter idParameter = new SqlParameter();
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Value = customer.CustomerID;
                idParameter.ParameterName = "@USERID";
                FindOrders.Parameters.Add(idParameter);

                Console.WriteLine(" Order Number  |  Product Name | Amount Ordered| Total Price  ");
                sqlConnection.Open();
                SqlDataReader sqlDataReader = FindOrders.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    tmp = Convert.ToString(sqlDataReader["order"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }

                    Console.Write(tmp);
                    tmp = (string)sqlDataReader["name"];
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["amount"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["price"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.WriteLine(tmp);
                }
                sqlConnection.Close();
            }
        }

        static public void PrintAllProducts()
        {
            string tmp;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand FindOrders = new SqlCommand();
                FindOrders.Connection = sqlConnection;
                FindOrders.CommandText = "SHOW_ALL_PRODUCTS_CUSTOMER";
                FindOrders.CommandType = CommandType.StoredProcedure;

                Console.WriteLine(" Product Number|  Product Name |   Inventory   |    Price   ");
                sqlConnection.Open();
                SqlDataReader sqlDataReader = FindOrders.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    tmp = Convert.ToString(sqlDataReader["id"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }

                    Console.Write(tmp);
                    tmp = (string)sqlDataReader["name"];
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["inventory"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["price"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.WriteLine(tmp);
                }
                sqlConnection.Close();
            }
        }

        static public void PrintSupplierItems(Supplier supplier)
        {
            if (supplier.ID == 0)
            {
                throw new InsufficientDataException();
            }
            string tmp;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {
                SqlCommand FindOrders = new SqlCommand();
                FindOrders.Connection = sqlConnection;
                FindOrders.CommandText = "SHOW_ALL_PRODUCTS_SUPPLIER";
                FindOrders.CommandType = CommandType.StoredProcedure;

                SqlParameter idParameter = new SqlParameter();
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Value = supplier.ID;
                idParameter.ParameterName = "@SUPPLIER_ID";
                FindOrders.Parameters.Add(idParameter);

                Console.WriteLine(" Product Number|  Product Name |   Inventory   |    Price   ");
                sqlConnection.Open();
                SqlDataReader sqlDataReader = FindOrders.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    tmp = Convert.ToString(sqlDataReader["id"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }

                    Console.Write(tmp);
                    tmp = (string)sqlDataReader["name"];
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["inventory"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.Write(tmp);
                    tmp = Convert.ToString(sqlDataReader["price"]);
                    if (tmp.Length < 15)
                    {
                        tmp = tmp.PadLeft(15).Substring(0, 15);
                    }
                    Console.Write("|");
                    Console.WriteLine(tmp);
                }
                sqlConnection.Close();
            }
        }
    }
}
