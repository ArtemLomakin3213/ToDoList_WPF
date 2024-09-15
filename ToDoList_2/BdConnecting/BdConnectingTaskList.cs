using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_2.BdConnecting
{
    class BdConnectingTaskList : IServiceTask
    {
        private readonly string _connectionString;
        public BdConnectingTaskList()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }

        public void addTask(Task task)
        {
            string query = "INSERT INTO Tasks (user_id, title, description, due_date, is_completed) VALUES (@UserId, @Title, @Description, @DueDate, @IsCompleted)";

            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", task.UserId);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@DueDate", (object)task.DueDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void deleteTask(int task_id)
        {
            string query = "DELETE FROM Tasks WHERE task_id = @TaskId";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TaskId", task_id);
                command.ExecuteNonQuery();
            }
        }

        public void updateTask(Task task)
        {
            string query = "UPDATE Tasks SET title = @Title, description = @Description, due_date = @DueDate, is_completed = @IsCompleted WHERE task_id = @TaskId";
            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TaskId", task.TaskId);
                command.Parameters.AddWithValue("@Title", task.Title);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@DueDate", (object)task.DueDate ?? DBNull.Value);
                command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception("Задача не была обновлена.");
                }
            }
        }
        public List<Task> GetTasks(int user_id)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT task_id, title, description, due_date, is_completed FROM Tasks WHERE user_id = @User_Id";

            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@User_Id", user_id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task task = new Task
                        {
                            TaskId = (int)reader["task_id"],
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            DueDate = reader["due_date"] != DBNull.Value ? (DateTime?)reader["due_date"] : null,
                            IsCompleted = (bool)reader["is_completed"]
                        };
                        tasks.Add(task);
                    }
                }
            }

            return tasks;
        }
        public List<Task> GetTask(int task_id)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT title, description, due_date, is_completed FROM Tasks WHERE task_id = @Task_Id";

            using (SqlConnection connection = OpenConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Task_Id", task_id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Task task = new Task
                        {
                            Title = reader["title"].ToString(),
                            Description = reader["description"].ToString(),
                            DueDate = reader["due_date"] != DBNull.Value ? (DateTime?)reader["due_date"] : null,
                            IsCompleted = (bool)reader["is_completed"]
                        };
                        tasks.Add(task);
                    }
                }
            }

            return tasks;

        }
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
