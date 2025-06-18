using BulletJournalApp.Core.Interface;
using BulletJournalApp.Core.Services;
using BulletJournalApp.Library;
using BulletJournalApp.Library.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BulletJournalApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IPriorityService _priorityService;
        private readonly ICategoryService _categoryService;
        private readonly IScheduleService _scheduleService;
        private readonly ITasksStatusService _tasksStatusService;
        public TasksController(
            ITaskService taskService, IPriorityService priorityService, ICategoryService categoryService, IScheduleService scheduleService, ITasksStatusService tasksStatusService)
        {
            _taskService = taskService;
            _priorityService = priorityService;
            _categoryService = categoryService;
            _scheduleService = scheduleService;
            _tasksStatusService = tasksStatusService;
        }
        [HttpPost]
        public IActionResult PostTask(Tasks task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Task is null or title is empty.");
            }
            var result = _taskService.AddTask(task);
            if (result)
            {
                return CreatedAtAction(nameof(GetTaskByTitle), new { title = task.Title }, task);
            }
            return BadRequest("Failed to add task.");
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskService.ListAllTasks();
            return Ok(tasks);
        }

        [HttpGet("incomplete")]
        public IActionResult GetIncompleteTasks()
        {
            var tasks = _taskService.ListIncompleteTasks();
            return Ok(tasks);
        }

        [HttpGet("{title}")]
        public IActionResult GetTaskByTitle(string title)
        {
            var task = _taskService.FindTasksByTitle(title);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("priority/{priority}")]
        public IActionResult GetTasksByPriority(string priority)
        {
            if (!Enum.TryParse(priority, true, out Priority parsedPriority))
            {
                return BadRequest("Invalid priority value.");
            }
            var tasks = _priorityService.ListTasksByPriority(parsedPriority);
            return Ok(tasks);
        }

        [HttpGet("category/{category}")]
        public IActionResult GetTasksByCategory(string category)
        {
            if (!Enum.TryParse(category, true, out Category parsedCategory))
            {
                return BadRequest("Invalid category value.");
            }
            var tasks = _categoryService.ListTasksByCategory(parsedCategory);
            return Ok(tasks);
        }
        [HttpGet("schedule/{schedule}")]
        public IActionResult GetTasksBySchedule(string schedule)
        {
            if (!Enum.TryParse(schedule, true, out Schedule parsedSchedule))
            {
                return BadRequest("Invalid schedule value.");
            }
            var tasks = _scheduleService.ListTasksBySchedule(parsedSchedule);
            return Ok(tasks);
        }
        [HttpGet("status/{status}")]
        public IActionResult GetTasksByStatus(string status)
        {
            if (!Enum.TryParse(status, true, out TasksStatus parsedStatus))
            {
                return BadRequest("Invalid status value.");
            }
            var tasks = _tasksStatusService.ListTasksByStatus(parsedStatus);
            return Ok(tasks);
        }
        [HttpPut("mark-complete/{title}")]
        public IActionResult MarkTaskComplete(Tasks task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Task is null or title is empty.");
            }
            _taskService.MarkTasksComplete(task.Title);
            return NoContent();
        }
        [HttpPut("update/{oldTitle}")]
        public IActionResult UpdateTask(string oldTitle, [FromBody] Tasks updatedTask)
        {
            if (updatedTask == null || string.IsNullOrEmpty(updatedTask.Title))
            {
                return BadRequest("Updated task is null or title is empty.");
            }
            _taskService.UpdateTask(oldTitle, updatedTask.Title, updatedTask.Description, updatedTask.Notes, updatedTask.DueDate);
            return NoContent();
        }
        [HttpDelete("{title}")]
        public IActionResult DeleteTask(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title is empty.");
            }
            _taskService.DeleteTask(title);
            return NoContent();
        }
        [HttpPut("priority/{priority}")]
        public IActionResult UpdatePriority(string title, [FromBody] int priority)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return BadRequest($"title is null or empty");
            }
            _priorityService.ChangePriority(title, (Priority) priority);
            return NoContent();
        }
        [HttpPut("category/{category}")]
        public IActionResult UpdateCategory(string title, [FromBody] int category)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return BadRequest("title is empty or null");
            }
            _categoryService.ChangeCategory(title, (Category) category);
            return NoContent();
        }
        [HttpPut("status/{status}")]
        public IActionResult UpdateStatus(string title, [FromBody] int status)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return BadRequest($"title is null or empty");
            }
            _tasksStatusService.ChangeStatus(title, (TasksStatus)status);
            return NoContent();
        }
        [HttpPut("schedule/{schedule}")]
        public IActionResult UpdateSchedule(string title, [FromBody] int schedule)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return BadRequest("Title is null or empty");
            }
            _scheduleService.ChangeSchedule(title, (Schedule) schedule);
            return NoContent();
        }
    }
}
