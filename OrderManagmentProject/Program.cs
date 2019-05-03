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
            string connectionString = "data source =.; database = OrderManagment; integrated security = true ";
            Customer customer = new Customer("A", "W", "ert", "hfg", 12);
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlDBConnection"].ConnectionString))
            {

                SqlCommand AddnewUser = new SqlCommand();
                AddnewUser.Connection = connection;
                AddnewUser.CommandText = "ADD_NEW_CUSTOMER";
                AddnewUser.CommandType = CommandType.StoredProcedure;

                SqlParameter username = new SqlParameter();
                username.SqlDbType = SqlDbType.VarChar;
                username.Value = cust.Username;
                username.ParameterName = "@USERNAME";
                AddnewUser.Parameters.Add(username);

                SqlParameter password = new SqlParameter();
                password.SqlDbType = SqlDbType.VarChar;
                password.Value = cust.Password;
                password.ParameterName = "@PASSWORD";
                AddnewUser.Parameters.Add(password);

                SqlParameter firstName = new SqlParameter();
                firstName.SqlDbType = SqlDbType.VarChar;
                firstName.Value = cust.FirstName;
                firstName.ParameterName = "@FIRSTNAME";
                AddnewUser.Parameters.Add(firstName);

                SqlParameter lastName = new SqlParameter();
                lastName.SqlDbType = SqlDbType.VarChar;
                lastName.Value = cust.LastName;
                lastName.ParameterName = "@LASTNAME";
                AddnewUser.Parameters.Add(lastName);

                SqlParameter creditNumber = new SqlParameter();
                creditNumber.SqlDbType = SqlDbType.Int;
                creditNumber.Value = cust.CreditNumber;
                creditNumber.ParameterName = "@CREDITNUMBER";
                AddnewUser.Parameters.Add(creditNumber);

                connection.Open();
                AddnewUser.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

