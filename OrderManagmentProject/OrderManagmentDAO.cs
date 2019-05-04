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

        static public bool DoesCustomerExist(Customer customer)
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

        static public int FindUserId(Customer customer)
        {
            if (DoesCustomerExist(customer) == false)
            {
                throw new UserDoesNotExistException();
            }

            int customerID = 0;

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
            return customerID;
        }

        static public void AddNewCustomer (Customer customer)
        {
            if (DoesCustomerExist(customer) == true)
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
            }
        }
    }
}
