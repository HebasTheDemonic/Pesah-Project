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
            string username = "BROK";
            string password = "BROK";
            int customerID;
            string connectionString = "data source =.; database = Order_Managment; integrated security = true ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand FindUserID = new SqlCommand();
                FindUserID.Connection = connection;
                FindUserID.CommandText = "FIND_CUSTOMER_ID";
                FindUserID.CommandType = CommandType.StoredProcedure;

                SqlParameter usernameParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = username;
                usernameParameter.ParameterName = "@username";

                SqlParameter passwordParameter = new SqlParameter();
                usernameParameter.SqlDbType = SqlDbType.VarChar;
                usernameParameter.SqlValue = password;
                usernameParameter.ParameterName = "@password";

                FindUserID.Parameters.Add(passwordParameter);
                FindUserID.Parameters.Add(usernameParameter);

                connection.Open();
                SqlDataReader sqlDataReader = FindUserID.ExecuteReader();
                
                customerID = (int)sqlDataReader["ID"];

            }
        }
    }
}
