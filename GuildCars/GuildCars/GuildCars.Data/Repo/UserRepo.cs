using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models;

namespace GuildCars.Data.Repo
{
    public class UserRepo : IUserRepo
    {
        public IEnumerable<User> GetAll()
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = "Server=localhost;Database=GuildCars;Trusted_Connection=True";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUsers";

            conn.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    User user = new User();

                    user.UserId = dr["UserID"].ToString();
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Role = dr["Role"].ToString();

                    yield return user;
                }
            }
            yield break;
        }

        public User GetById(string id)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = "Server=localhost;Database=GuildCars;Trusted_Connection=True";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetUserById";

            cmd.Parameters.AddWithValue("@UserId", id);

            conn.Open();

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    User user = new User();

                    user.UserId = dr["UserID"].ToString();
                    user.FirstName = dr["FirstName"].ToString();
                    user.LastName = dr["LastName"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.Role = dr["Role"].ToString();

                    return user;
                }
            }
            return null;
        }
    }
}
