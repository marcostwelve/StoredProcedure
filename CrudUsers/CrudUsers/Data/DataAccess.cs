using System.Data;
using System.Data.SqlClient;
using CrudUsers.Models;

namespace CrudUsers.Data
{
    public class DataAccess
    {
        [Obsolete("This method is deprecated, use GetConnection() instead.")]
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<User> ListAllUsers()
        {
            List<User> users = new List<User>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[LIST_ALL_USERS]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read()) 
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.UserName = reader["UserName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Position = reader["Position"].ToString();
                    users.Add(user);
                }

                _connection.Close();
            }

            return users;

        }

        public bool InsertUser(User user)
        {
            int id = 0;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[INSERT_USER]";

                _command.Parameters.AddWithValue("@UserName", user.UserName);
                _command.Parameters.AddWithValue("@LastName", user.LastName);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Position", user.Position);

                _connection.Open();

                id = _command.ExecuteNonQuery();

                _connection.Close();
            }

            return id > 0 ? true : false;
        }

        public User GetUserById(int id)
        {
            var user = new User();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[LIST_USER_BY_ID]";
                _command.Parameters.AddWithValue("@Id", id);

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    user.Id = Convert.ToInt32(reader["Id"]);
                    user.UserName = reader["UserName"].ToString();
                    user.LastName = reader["LastName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Position = reader["Position"].ToString();
                }

                _connection.Close();
            }

            return user;
        }

        public bool UpdateUser(User user)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[EDIT_USER]";
                _command.Parameters.AddWithValue("@Id", user.Id);
                _command.Parameters.AddWithValue("@UserName", user.UserName);
                _command.Parameters.AddWithValue("@LastName", user.LastName);
                _command.Parameters.AddWithValue("@Email", user.Email);
                _command.Parameters.AddWithValue("@Position", user.Position);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool DeleteUser(int idUser)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[REMOVE_USER]";
                _command.Parameters.AddWithValue("@Id", idUser);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }

            return id > 0 ? true : false;
        }
    }
}
