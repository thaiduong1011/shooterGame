using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace Server2.Controllers
{
    public class ScoreController : ApiController
    {

        public String GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DbGameCuoiKy"].ConnectionString;
        }

        [HttpGet]
        public List<object> GetListScoreByUserId(int user_id)
        {
            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();

            var command = "select * from scores where user_id = @user_id";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@user_id", user_id);
            SqlDataReader reader = cmd.ExecuteReader();
            List<object> scorelist = new List<object>();

            while (reader.Read())
            {
                scorelist.Add(new
                {
                    user_id = reader["user_id"],
                    score = reader["score"]

                });
            }
            return scorelist.ToList();
        }

        [HttpGet]
        public List<object> GetListScore()
        {
            var cnnString = this.GetConnectionString();
            SqlConnection cnn = new SqlConnection(cnnString);
            cnn.Open();

            var command = "select scores.score, users.user_id, users.user_name from(users inner join scores on scores.user_id = users.user_id)";
            SqlCommand cmd = new SqlCommand(command, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            List<object> scorelist = new List<object>();

            while (reader.Read())
            {
                scorelist.Add(new
                {
                    user_id = reader["user_id"],
                    user_name = reader["user_name"],
                    score = reader["score"]

                });
            }
            return scorelist.ToList();
        }


        [HttpPost]
        public int InsertScoreByUsername(int user_id, int score)
        {
            if (user_id == 0 || score == 0)
            {
                return -1;
            }

            try
            {
                var cnnString = this.GetConnectionString();
                var command = "insert into scores(user_id, score) values(@user_id, @score)";
                using (SqlConnection cnn = new SqlConnection(cnnString))
                {
                    using (SqlCommand cmd = new SqlCommand(command, cnn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user_id);
                        cmd.Parameters.AddWithValue("@score", score);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
