using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SuccessFactors.Models
{
    public class WebRoleProvide : RoleProvider
    {
        public static string getConnectionstring(string connectionName = "SuccessF")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<String> roles = new List<String>();
            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"Select roletype from role_table where roleid in (Select roleid from tc_users where email = '{@HttpContext.Current.User.Identity.Name}');", con);
                command.Connection.Open();
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        roles.Add(dr.GetString(0));
                    }
                    command.Connection.Close();
                }
            }
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}