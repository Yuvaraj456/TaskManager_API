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
    }
}
