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
    }
}