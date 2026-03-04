using System;
using System.Collections.Generic;
using System.Text;
using TaskList.Crosscutting.Dtos;
using TaskList.Crosscutting.Models;
using TaskList.Data.Repositories;

namespace TaskList.Domain.Orchestrators
{
    public class TasksOrchestrator
    {
        private TasksRepository repository;

        public TasksOrchestrator()
        {
            repository = new TasksRepository();
        }

        public List<TaskModel> GetAllTasks()
        {
            List<TaskModel> taskModels = new List<TaskModel>();
            List<TaskDto> taskDtos = repository.GetAllTasks();

            foreach (var taskDto in taskDtos)
            {
                TaskModel taskModel = new TaskModel
                {
                    Id = taskDto.Id,
                    CreationDate = taskDto.CreationDate,
                    Description = taskDto.Description,
                    DueDate = taskDto.DueDate,
                    IsCompleted = taskDto.IsCompleted
                };
                taskModels.Add(taskModel);
            }

            return taskModels;
        }
    }
}
