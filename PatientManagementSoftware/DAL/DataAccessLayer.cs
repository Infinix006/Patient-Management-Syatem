using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
//using System.Configuration;

namespace PatientManagementSoftware.DAL
{
    public class DataAccessLayer
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["db_PMSConnection"].ConnectionString;

        public DataAccessLayer() { 
        
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DataSet ExecuteDataSet(string query)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataSet);
            }

            return dataSet;
        }

        public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
    }
}