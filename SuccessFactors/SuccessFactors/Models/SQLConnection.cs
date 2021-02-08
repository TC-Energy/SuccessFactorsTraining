using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Microsoft.IdentityModel.Protocols;

namespace SuccessFactors.Models
{
    public static class SQLConnection
    {
        public static string getConnectionstring(string connectionName = "SuccessF")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> loadDate<T>(string sql)
        {
            using(SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                return con.Query<T>(sql).ToList();
            }
        }


        public static void EnrollCommand(string queryString, string id, string status, string course)
        {
            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Courses", course);
                command.ExecuteNonQuery();
                command.Connection.Close();
            }

        }

    }
}