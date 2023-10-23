
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;

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

        [HttpGet]
        [Route("[action]/{searchBy}/{searchString?}")]
        public async Task<List<Project>> Search([FromRoute] string? searchBy, [FromRoute] string? searchString)
        {
            if (searchBy == null)
                return null;

            if (searchString == null)
                return await Get();

            //List<Project>? searchResult = (searchBy) switch
            //{
            //    "ProjectId" => await _db.Projects.Where(x => x.ProjectId.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToListAsync(),

            //    "ProjectName" => await _db.Projects.Where(x => x.ProjectName.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToListAsync(),

            //    "DateOfStart" => await _db.Projects.Where(x => x.DateOfStart.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToListAsync(),

            //    "TeamSize" => await _db.Projects.Where(x => x.TeamSize.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToListAsync(),
            //    _ => null
            //};

            List<Project>? searchResult = null;
             switch(searchBy)
            {
                case "ProjectId":
                    searchResult = await _db.Projects.Where(x => x.ProjectId.ToString().Contains(searchString)).ToListAsync();
                        break;

                case "ProjectName":
                    searchResult = await _db.Projects.Where(x => x.ProjectName.Contains(searchString)).ToListAsync();
                        break;

                case "DateOfStart":
                    searchResult = await _db.Projects.Where(x => x.DateOfStart.ToString().Contains(searchString)).ToListAsync();
                        break;

                case "TeamSize":
                    searchResult = await _db.Projects.Where(x => x.TeamSize.ToString().Contains(searchString)).ToListAsync();
                        break;

                default:                    
                    break;
            }




            if (searchResult != null)
            {
                return searchResult;
            }
            else
            {
                return null;
            }
            
        }
    }
}
