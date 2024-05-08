using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksService _tasksService;

        public TasksController(TasksService tasksService) =>
            _tasksService = tasksService;

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<List<TaskItem>> Get()
        {
            return await _tasksService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TaskItem>> Get(string id)
        {
            var task = await _tasksService.GetAsync(id);

            if (task is null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TaskItem newTaskItem)
        {
            await _tasksService.CreateAsync(newTaskItem);

            return CreatedAtAction(nameof(Get), new { id = newTaskItem.Id }, newTaskItem);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Put(string id, TaskItem updatedTaskItem)
        {
            var taskItem = await _tasksService.GetAsync(id);

            if (taskItem is null)
            {
                return NotFound();
            }

            updatedTaskItem.Id = taskItem.Id;

            await _tasksService.UpdateAsync(id, updatedTaskItem);

            return NoContent();
        }

    }
}
