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
            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                return con.Query<T>(sql).ToList();
            }
        }

        public static void checkUserExists(string queryString, int User_Id, string First_Name, string Last_Name, string User_name, string Password, int RoleId, string Email)
        {
            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Id", Convert.ToInt32(User_Id));
                command.Parameters.AddWithValue("@First_Name", First_Name);
                command.Parameters.AddWithValue("@Last_Name", Last_Name);
                command.Parameters.AddWithValue("@User_name", User_name);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@RoleId", RoleId);
                command.Parameters.AddWithValue("@Email", Email);
                command.ExecuteNonQuery();
                command.Connection.Close();
            }

        }

        public static bool ValidateExternalUsers(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM External_Users WHERE (Username='{@username}' OR Email='{username}') AND Password = '{@password}';", con);
                command.Connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    command.Connection.Close();
                    return true;
                }
                else
                {
                    command.Connection.Close();
                    return false;
                }

            }
        }
    }
}