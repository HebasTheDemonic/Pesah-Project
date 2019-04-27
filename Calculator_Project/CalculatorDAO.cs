using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Project
{
    public static class CalculatorDAO 
    {
        static string connectionString = "Data Source = .; Database = Calc_Project; Integrated Security = True";

        static CalculatorDAO()
        {

        }

        internal static void AddInputLine(double X, double Y)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "ADD_INPUTS";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter_X = new SqlParameter();
                parameter_X.ParameterName = "@X";
                parameter_X.SqlDbType = SqlDbType.Float;
                parameter_X.Direction = ParameterDirection.Input;
                parameter_X.Value = X;

                SqlParameter parameter_Y = new SqlParameter();
                parameter_Y.ParameterName = "@Y";
                parameter_Y.SqlDbType = SqlDbType.Float;
                parameter_Y.Direction = ParameterDirection.Input;
                parameter_Y.Value = Y;

                command.Parameters.Add(parameter_X);
                command.Parameters.Add(parameter_Y);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void CalculateLines()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "CALCULATE";
                command.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void ClearDB()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "CLEAR_DB";
                command.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void ClearInputOnly()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "CLEAR_INPUT";
                command.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void ClearResultsOnly()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "CLEAR_RESULTS";
                command.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        internal static void RetrieveResults(out double[] results)
        {
            int size;
            int index = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sizeCommand = new SqlCommand();
                sizeCommand.Connection = sqlConnection;
                sizeCommand.CommandText = "NUMBER_OF_RESULTS";
                sizeCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sizeParameter = new SqlParameter();
                sizeParameter.ParameterName = "@Size";
                sizeParameter.SqlDbType = SqlDbType.Int;
                sizeParameter.Direction = ParameterDirection.Output;

                sizeCommand.Parameters.Add(sizeParameter);

                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = sqlConnection;
                selectCommand.CommandText = "SELECT * FROM Results";
                selectCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                sizeCommand.ExecuteNonQuery();
                size = (int)sizeParameter.Value;
                results = new double[size];
                SqlDataReader sqlDataReader = selectCommand.ExecuteReader();
                while (sqlDataReader.Read() == true)
                {
                    results[index] = (double)sqlDataReader[0];
                    index++;
                }

                sqlConnection.Close();
            }
        }

        internal static void PrintCalc()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand printCommand = new SqlCommand();
                printCommand.Connection = sqlConnection;
                printCommand.CommandText = "select Calc.X as X_Input, Calc.Operation as Operator, Calc.Y as Y_Input, Calc.Results as Results from Calc";
                printCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                SqlDataReader sqlDataReader = printCommand.ExecuteReader();

                while (sqlDataReader.Read() == true)
                {
                    Console.WriteLine($" {sqlDataReader["X_Input"]} {sqlDataReader["Operator"]} {sqlDataReader["Y_Input"]} = {sqlDataReader["Results"]}");
                }
            }
        }
    }
}
