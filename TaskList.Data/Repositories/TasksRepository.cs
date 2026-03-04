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

            return tasks;
        }
    }
}
