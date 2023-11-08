
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;
using TaskManager_Core.DTO;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProjectsController(ApplicationDbContext db)
        {

            _db = db;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<List<ProjectViewModel>> Get()
        {
            System.Threading.Thread.Sleep(1000); //make delay of 1 sec
            List<Project> project = await _db.Projects.Include("ClientLocation").ToListAsync();

            List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
            foreach (var item in project)
            {
                projectViewModel.Add(new ProjectViewModel()
                {
                     ProjectId = item.ProjectId,
                     ClientLocation = item.ClientLocation,
                     ClientLocationId = item.ClientLocationId,
                     DateOfStart = item.DateOfStart.ToString("dd/MM/yyyy"),
                     ProjectName = item.ProjectName,
                     Status = item.Status,
                     TeamSize = item.TeamSize,
                     Active = item.Active,
                });
            }

            return projectViewModel;
        }

        [HttpPost]
        [Route("[action]")] 
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();

            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            Project? addedProject = await _db.Projects.Include("ClientLocation").Where(temp=>temp.ProjectId == project.ProjectId).FirstOrDefaultAsync();

            if (addedProject == null)
            {
                return BadRequest("Projects not found");
            }

            ProjectViewModel projectViewModel = new ProjectViewModel()               
                {
                    ProjectId = addedProject.ProjectId,
                    ClientLocation = addedProject.ClientLocation,
                    ClientLocationId = addedProject.ClientLocationId,
                    DateOfStart = addedProject.DateOfStart.ToString("dd/MM/yyyy"),
                    ProjectName = addedProject.ProjectName,
                    Status = addedProject.Status,
                    TeamSize = addedProject.TeamSize,
                    Active = addedProject.Active,
                };
            
            return Ok(projectViewModel);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();

            Project? exist = await _db.Projects.Where(x => x.ProjectId == project.ProjectId).FirstOrDefaultAsync();

            if (exist != null)
            {
                exist.ProjectName = project.ProjectName;
                exist.DateOfStart = project.DateOfStart;
                exist.TeamSize = project.TeamSize;
                exist.Active = project.Active;
                exist.ClientLocation = project.ClientLocation;
                exist.Status = project.Status;
                exist.ClientLocation = null;
                await _db.SaveChangesAsync();

                Project? existProject = await _db.Projects.Include("ClientLocation").Where(temp => temp.ProjectId == project.ProjectId).FirstOrDefaultAsync();

                if (existProject == null)
                {
                    return BadRequest("Projects not found");
                }

                ProjectViewModel projectViewModel = new ProjectViewModel()
                {
                    ProjectId = existProject.ProjectId,
                    ClientLocation = existProject.ClientLocation,
                    ClientLocationId = existProject.ClientLocationId,
                    DateOfStart = existProject.DateOfStart.ToString("dd/MM/yyyy"),
                    ProjectName = existProject.ProjectName,
                    Status = existProject.Status,
                    TeamSize = existProject.TeamSize,
                    Active = existProject.Active,
                };

                return Ok(projectViewModel);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int? id)
        {
            if (id == null)
                return BadRequest();

            Project? delete = await _db.Projects.Where(x => x.ProjectId == id).FirstOrDefaultAsync();

            if (delete != null)
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
        public async Task<List<ProjectViewModel>> Search([FromRoute] string? searchBy, [FromRoute] string? searchString)
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
            switch (searchBy)
            {
                case "ProjectId":
                    searchResult = await _db.Projects.Include("ClientLocation").Where(x => x.ProjectId.ToString().Contains(searchString)).ToListAsync();
                    break;

                case "ProjectName":
                    searchResult = await _db.Projects.Include("ClientLocation").Where(x => x.ProjectName.Contains(searchString)).ToListAsync();
                    break;

                case "DateOfStart":
                    searchResult = await _db.Projects.Include("ClientLocation").Where(x => x.DateOfStart.ToString().Contains(searchString)).ToListAsync();
                    break;

                case "TeamSize":
                    searchResult = await _db.Projects.Include("ClientLocation").Where(x => x.TeamSize.ToString().Contains(searchString)).ToListAsync();
                    break;

                default:
                    break;
            }




            if (searchResult != null)
            {
                List<ProjectViewModel> projectViewModel = new List<ProjectViewModel>();
                foreach (var item in searchResult)
                {
                    projectViewModel.Add(new ProjectViewModel()
                    {
                        ProjectId = item.ProjectId,
                        ClientLocation = item.ClientLocation,
                        ClientLocationId = item.ClientLocationId,
                        DateOfStart = item.DateOfStart.ToString("dd/MM/yyyy"),
                        ProjectName = item.ProjectName,
                        Status = item.Status,
                        TeamSize = item.TeamSize,
                        Active = item.Active,
                    });
                }

                return projectViewModel;
            }
            else
            {
                return null;
            }

        }
    }
}
