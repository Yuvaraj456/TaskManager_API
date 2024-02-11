using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskPrioritiesController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public TaskPrioritiesController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("[action]")]
        public async Task<List<TaskPriority>> GetTaskPriorities()
        {
            List<TaskPriority> taskPriorities = await _db.TaskPriorities.ToListAsync();

            return taskPriorities;
        }


        [HttpGet("[action]/{taskPriorityId}")]
        public async Task<IActionResult> GetByTaskPriorityId([FromRoute] int taskPriorityId)
        {
            TaskPriority? taskPriority = await _db.TaskPriorities.FirstOrDefaultAsync(x => x.TaskPriorityId == taskPriorityId);

            if (taskPriority != null)
                return Ok(taskPriority);

            return BadRequest($"Task Priority not found, for this taskPriorityId - {taskPriorityId}");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTaskPriority([FromBody] TaskPriority taskPriority)
        {
            if (taskPriority == null)
                return BadRequest("Task Priority Object is empty");

            TaskPriority? isExist = await _db.TaskPriorities.FirstOrDefaultAsync(x => x.TaskPriorityName == taskPriority.TaskPriorityName);

            if (isExist != null)
                return BadRequest($"TaskPriority already exist - {taskPriority.TaskPriorityName}");

            await _db.TaskPriorities.AddAsync(taskPriority);
            await _db.SaveChangesAsync();

            return Ok(taskPriority);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTaskPriority([FromBody] TaskPriority taskPriority)
        {
            if (taskPriority == null)
                return BadRequest("Task Priority Object is empty");

            TaskPriority? isTaskPriorityExist = await _db.TaskPriorities.FirstOrDefaultAsync(x => x.TaskPriorityId == taskPriority.TaskPriorityId);

            if (isTaskPriorityExist == null)
                return BadRequest($"Task Priority not exist, unable to update - {taskPriority.TaskPriorityName}");

            //update
            isTaskPriorityExist.TaskPriorityName = taskPriority.TaskPriorityName;
            await _db.SaveChangesAsync();

            return Ok(taskPriority);
        }

        [HttpDelete("[action]/{taskPriorityId}")]
        public async Task<IActionResult> DeleteTaskPriority([FromRoute] int taskPriorityId)
        {
            if (taskPriorityId == 0)
                return BadRequest("taskPriorityId is invalid or empty");

            TaskPriority? isExist = await _db.TaskPriorities.FirstOrDefaultAsync(x => x.TaskPriorityId == taskPriorityId);

            if (isExist == null)
                return BadRequest($"Task Priority not exist, unable to delete - {taskPriorityId}");

            //delete
            _db.TaskPriorities.Remove(isExist);
            await _db.SaveChangesAsync();

            return Ok(true);
        }


    }
}
