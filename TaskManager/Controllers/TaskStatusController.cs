using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using Model =  TaskManager_Core.Domain.Entities;

namespace TaskManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public TaskStatusController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("[action]")]
        public async Task<List<Model.TaskStatus>> GetTaskStatus()
        {
            List<Model.TaskStatus> taskStatuses = await _db.TaskStatus.ToListAsync();

            return taskStatuses;
        }


        [HttpGet("[action]/{taskStatusId}")]
        public async Task<IActionResult> GetByTaskStatusId([FromRoute] int taskStatusId)
        {
            Model.TaskStatus? taskStatus = await _db.TaskStatus.FirstOrDefaultAsync(x => x.TaskStatusId == taskStatusId);

            if (taskStatus != null)
                return Ok(taskStatus);

            return BadRequest($"Task Status not found, for this taskStatusId - {taskStatusId}");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddTaskStatus([FromBody] Model.TaskStatus taskStatus)
        {
            if (taskStatus == null)
                return BadRequest("Task Status Object is empty");

            Model.TaskStatus? isExist = await _db.TaskStatus.FirstOrDefaultAsync(x => x.TaskStatusName == taskStatus.TaskStatusName);

            if (isExist != null)
                return BadRequest($"TaskStatus already exist - {taskStatus.TaskStatusName}");

            await _db.TaskStatus.AddAsync(taskStatus);
            await _db.SaveChangesAsync();

            return Ok(taskStatus);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] Model.TaskStatus taskStatus)
        {
            if (taskStatus == null)
                return BadRequest("Task Status Object is empty");

            Model.TaskStatus? isTaskStatusExist = await _db.TaskStatus.FirstOrDefaultAsync(x => x.TaskStatusId == taskStatus.TaskStatusId);

            if (isTaskStatusExist == null)
                return BadRequest($"Task Status not exist, unable to update - {taskStatus.TaskStatusName}");

            //update
            isTaskStatusExist.TaskStatusName = taskStatus.TaskStatusName;
            await _db.SaveChangesAsync();

            return Ok(taskStatus);
        }

        [HttpDelete("[action]/{taskStatusId}")]
        public async Task<IActionResult> DeleteTaskStatus([FromRoute] int taskStatusId)
        {
            if (taskStatusId == 0)
                return BadRequest("taskStatusId is invalid or empty");

            Model.TaskStatus? isExist = await _db.TaskStatus.FirstOrDefaultAsync(x => x.TaskStatusId == taskStatusId);

            if (isExist == null)
                return BadRequest($"Task Status not exist, unable to delete - {taskStatusId}");

            //delete
            _db.TaskStatus.Remove(isExist);
            await _db.SaveChangesAsync();

            return Ok(true);
        }

    }
}
