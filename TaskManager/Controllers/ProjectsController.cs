using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProjectsController(ApplicationDbContext db) {

            _db=db;
        }

        
        [HttpGet]
        [Route("[action]")]
        public async Task<List<Project>> Get()
        {
            List<Project> project = await _db.Projects.ToListAsync();
            return project;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            if (project == null)
                return BadRequest();

            var postdata = await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
            return Ok(project);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();

            Project? exist = await _db.Projects.Where(x=>x.ProjectId == project.ProjectId).FirstOrDefaultAsync();

            if(exist != null)
            {
                exist.ProjectName = project.ProjectName;
                exist.DateOfStart = project.DateOfStart;
                exist.TeamSize = project.TeamSize;
                await _db.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }

            return Ok(exist);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery]int? id)
        {
            if(id == null)
                return BadRequest();

            Project? delete = await _db.Projects.Where(x=>x.ProjectId == id).FirstOrDefaultAsync();

            if(delete != null)
            {
                 _db.Projects.Remove(delete);
                await _db.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }

            return Ok(id);
        }
    }
}
