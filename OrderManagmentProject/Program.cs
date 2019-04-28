using System;
using System.Collections.Generic;
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
            Customer cust = new Customer("ASDAD", "edg", "ert", "hfg", 12);
            bool flag = false;
            using (SqlConnection connection = new SqlConnection($"{connectionString}; MultipleActiveResultSets = true"))
            {
                SqlCommand checkIfUserExists = new SqlCommand();
                checkIfUserExists.Connection = connection;
                checkIfUserExists.CommandType = CommandType.Text;
                checkIfUserExists.CommandText = $"SELECT * FROM CUSTOMERS WHERE '{cust.Username}' = CUSTOMERS.USERNAME";

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
                SqlDataReader sqlDataReader = checkIfUserExists.ExecuteReader();

                while (sqlDataReader.Read() == true)
                {
                    flag = true;
                }

                    

                if (flag)
                {
                    connection.Close();
                    throw new UserAlreadyExistsException();
                }

                AddnewUser.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

