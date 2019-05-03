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

        static int FindUserId(Customer customer)
        {
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
                }
                connection.Close();
            }
            return customerID;
        }

        static void AddNewCustomer (Customer customer)
        {

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
    }
}
