using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TaskList.Crosscutting.Models;
using TaskList.Domain.Orchestrators;

namespace TaskListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TasksController : ControllerBase
    {
        private TasksOrchestrator orchestrator;

        public TasksController()
        {
            orchestrator = new TasksOrchestrator();
        }

        [HttpGet]
        public List<TaskModel> GetTasks()
        {
            return orchestrator.GetAllTasks();
        }

        [HttpPost]
        public bool AddTask(TaskModel task)
        {
            return orchestrator.AddTask(task);
        }

        [HttpPut]
        public bool UpdateTask(TaskModel task)
        {
            return orchestrator.UpdateTask(task);
        }

        [HttpDelete("{id}")]
        public bool DeleteTask(int id)
        {
            return orchestrator.DeleteTask(id);
        }
    }
}
