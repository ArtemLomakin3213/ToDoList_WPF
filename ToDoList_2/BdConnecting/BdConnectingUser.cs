using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2.BdConnecting
{
    class BdConnectingUser : IServiceUser
    {
        private readonly string _connectionString;
        public BdConnectingUser()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool GetToName(string Username, string Password)
        {

            string commandString = "SELECT COUNT(*) FROM [Users] WHERE username = @Username AND password = @Password;";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        public void Registration(User user)
        {
            string commandString = "INSERT INTO [Users] (username, email, password) VALUES (@Username, @Email, @Password)";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.ExecuteNonQuery();
            }
        }
        public bool CheckUserBase(User user)
        {
            string commandString = "SELECT COUNT(*) FROM [Users] WHERE username = @Username";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        public bool CheckEmailBase(User user)
        {
            string commandString = "SELECT COUNT(*) FROM [Users] WHERE email = @Email";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@Email", user.Email);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        public int CheckIdBase(string Username)
        {
            string commandString = "SELECT user_id FROM Users WHERE username = @username;";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new(commandString, connection))
            {
                command.Parameters.AddWithValue("@username", Username);
                int user_id = (int)command.ExecuteScalar();
                return user_id;
            }
        }
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
