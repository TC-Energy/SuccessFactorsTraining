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

        public static void checkUserExists(string queryString, int User_Id, string First_Name, string Last_Name, string User_name, string Password, string RoleId, string Email)
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
                SqlCommand command = new SqlCommand($"SELECT * FROM ExternalUserCredentials WHERE (User_name='{@username}' OR Email='{username}') AND Password = '{@password}';", con);
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

        public static List<CourseInfo> studentAssignedCourses(string studentID)
        {
            var list = new List<CourseInfo>();

            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"SELECT a.CourseID, c.Title FROM AssignedStudentCourses a INNER JOIN Courses c ON a.CourseID = c.CourseID AND (a.TCUser_ID ='{studentID}';", con);
                command.Connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        list.Add(new CourseInfo
                        {
                            courseID = Convert.ToInt32(sdr["COURSEID"]),
                            title = sdr["Title"].ToString(),
                        });
                    }
                }

                command.Connection.Close();
            }

                return list;
        }

        public static List<InstructorInfo> InstructorAssignedCourses_Student(string courseID)
        {
            var list = new List<InstructorInfo>();

            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"SELECT t.First_Name, t.Last_Name FROM TC_Users t a INNER JOIN AssignedInstructorCourses a ON a.CourseID = {courseID} AND a.TCInstructorUser_ID = t.User_Id ;", con);
                command.Connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        list.Add(new InstructorInfo
                        {
                            first_name = sdr["First_Name"].ToString(),
                            last_name = sdr["Last_Name"].ToString(),
                        });
                    }
                }

                command.Connection.Close();
            }

            return list;
        }


        //sql connection
        public static List<SessionInfo> load_sessions(int userID, int courseID)
        {

            var list = new List<SessionInfo>();

            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"select CourseID, CreatedON, Location from Session WHERE TCInstructor_ID = '{userID}' AND COURSEID = {courseID};", con);
                command.Connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        list.Add(new SessionInfo
                        {
                            courseID = Convert.ToInt32(sdr["CourseID"]),
                            createdON = sdr["CreatedON"].ToString(),
                            location = sdr["Location"].ToString(),

                        });
                    }
                }

                command.Connection.Close();
            }

            return list;
        }


        public static List<CourseInfo> InstructorAssignedCourses(int instructorID)
        {
            var list = new List<CourseInfo>();

            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"SELECT c.Course_Number, c.Title, c.Course_Description FROM Courses c INNER JOIN AssignedInstructorCourses a ON a.CourseID = c.CourseID AND a.TCInstructorUser_ID = {instructorID} ;", con);
                command.Connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        list.Add(new CourseInfo
                        {
                            course_number = sdr["Course_Number"].ToString(),
                            title = sdr["Title"].ToString(),
                            course_description = sdr["Course_Description"].ToString(),

                        });
                    }
                }

                command.Connection.Close();
            }

            return list;
        }

        public static List<CourseInfo> InstructorTaughtCourses(int instructorID)
        {
            var list = new List<CourseInfo>();

            using (SqlConnection con = new SqlConnection(getConnectionstring()))
            {
                SqlCommand command = new SqlCommand($"SELECT c.Course_Number, c.Title FROM Courses c INNER JOIN AssignedInstructorCourses a ON a.CourseID = c.CourseID AND a.TCInstructorUser_ID = {instructorID} and a.Status = 'True';", con);
                command.Connection.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        list.Add(new CourseInfo
                        {
                            course_number = sdr["Course_Number"].ToString(),
                            title = sdr["Title"].ToString(),
                        });
                    }
                }

                command.Connection.Close();
            }

            return list;
        }

    }
}