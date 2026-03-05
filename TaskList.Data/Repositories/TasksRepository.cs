using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Data;
using TaskList.Crosscutting;
using TaskList.Crosscutting.Dtos;

namespace TaskList.Data.Repositories
{
    public class TasksRepository
    {
        private string Schema = @"dbo";
        private string ConnectionString;

        public TasksRepository()
        {
            ConnectionString = ConnectionStrings.GetConnectionString();
        }

        public List<TaskDto> GetAllTasks()
        {
            var tasks = new List<TaskDto>();

            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                using var command = new SqlCommand("GetTasks", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var dto = new TaskDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        DueDate = reader.IsDBNull("DueDate") ? null : reader.GetDateTime(reader.GetOrdinal("DueDate")),
                        IsCompleted = reader.GetBoolean("IsCompleted")
                    };
                    tasks.Add(dto);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }

            return tasks;
        }

        public bool AddTask(TaskDto dto)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                using var command = new SqlCommand("AddTask", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // make so it's no longer nullable if there is a due date
                DateTime finalDueDate = dto.DueDate ?? DateTime.Now;

                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@DueDate", dto.DueDate != null ? DateOnly.FromDateTime(finalDueDate) : null);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding product: {ex.Message}");
                return false;
            }

        }

        public bool UpdateTask(TaskDto dto)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();

                using var command = new SqlCommand("UpdateTask", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // make so it's no longer nullable if there is a due date
                DateTime finalDueDate = dto.DueDate ?? DateTime.Now;

                command.Parameters.AddWithValue("@Id", dto.Id);
                command.Parameters.AddWithValue("@Description", dto.Description);
                command.Parameters.AddWithValue("@DueDate", dto.DueDate != null ? DateOnly.FromDateTime(dto.DueDate.Value) : null);
                command.Parameters.AddWithValue("@IsCompleted", dto.IsCompleted);
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public bool DeleteTask(int id)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                using var command = new SqlCommand("DeleteTask", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
