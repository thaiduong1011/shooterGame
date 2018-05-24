using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace Server2.Controllers
{
    public class UserController : ApiController
    {
        public String GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DbGameCuoiKy"].ConnectionString;
        }

        [HttpGet]
        public List<object> GetUser(int id)
        {
            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();
            var command = "select * from users where user_id = @user_id";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@user_id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            List<object> user = new List<object>();

            if (!reader.HasRows)
            {
                return user;
            }

            while (reader.Read())
            {
                user.Add(new
                {
                    user_id = reader["user_id"],
                    user_name = reader["user_name"],
                    password = reader["password"],
                    updated_at = reader["updated_at"]
                });
            }

            return user.ToList();
        }

        [HttpPost]
        public int CheckLogin(string user_name, string password)
        {
            if (user_name == "" || password == "")
            {
                return -1;
            }

            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();
            var command = "select user_id from users where user_name = @user_name and password = @password";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@user_name", user_name);
            cmd.Parameters.AddWithValue("@password", password);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return -1;
            }

            while (reader.Read())
            {
                return Convert.ToInt32(reader["user_id"]);
            }

            return -1;
        }

        [HttpGet]
        public List<object> GetListUser()
        {
            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();

            var command = "select * from users";
            SqlCommand cmd = new SqlCommand(command, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<object> user = new List<object>();

            if (!reader.HasRows)
            {
                return user;
            }

            while (reader.Read())
            {
                user.Add(new
                {
                    user_id = reader["user_id"],
                    user_name = reader["user_name"],
                    password = reader["password"],
                    updated_at = reader["updated_at"]
                });
            }
            return user.ToList();
        }

        [HttpGet]
        public List<object> GetUserByUserName(string user_name)
        {
            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();
            var command = "select * from users where user_name = @user_name";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@user_name", user_name);

            SqlDataReader reader = cmd.ExecuteReader();
            List<object> user = new List<object>();

            if (!reader.HasRows)
            {
                return user;
            }

            while (reader.Read())
            {
                user.Add(new
                {
                    user_id = reader["user_id"],
                    user_name = reader["user_name"],
                    password = reader["password"],
                    updated_at = reader["updated_at"]
                });
            }

            return user.ToList();
        }

        [HttpPost]
        public int InsertUser(String user_name, String password, String email)
        {
            if (user_name == "" || password == "" || email == "")
            {
                return 2;
            }

            List<object> user = this.GetUserByUserName(user_name);
            bool isEmptyUser = !user.Any();

            //check duplicate
            //if (isEmptyUser)
            //{
              //  return 3;
            //}

            try
            {
                var cnnString = this.GetConnectionString();
                var command = "insert into users(user_name, password, email) values(@user_name,@password, @email)";
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(command, cnn))
                    {
                        cmd.Parameters.AddWithValue("@user_name", user_name);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                    }
 
                }               
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public int ChangePassword(String user_name, String old_password, String new_password)
        {
            if (user_name == "" || new_password == "" || old_password == new_password)
            {
                return 2;
            }

            List<object> user = this.GetUserByUserName(user_name);
            bool isEmptyUser = !user.Any();

            //check empty user
            if (isEmptyUser)
            {
                return 3;
            }

            try
            {
                var cnnString = this.GetConnectionString();
                var command = "update users set password = @password where user_name = @user_name";
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(command, cnn))
                    {
                        cmd.Parameters.AddWithValue("@user_name", user_name);
                        cmd.Parameters.AddWithValue("@password", new_password);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
