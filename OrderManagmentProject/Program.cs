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
            Customer customer = new Customer("A", "W", "ert", "hfg", 12);
            OrderManagmentDAO.FindUserId(customer);
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
            Console.ReadKey();
        }
    }
}

