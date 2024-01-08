using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientLocationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ClientLocationController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("get")]
        public async Task<ActionResult<List<ClientLocation>>> Get()
        {

            List<ClientLocation> locations = await _db.ClientLocations.ToListAsync();
            return locations;
        }

    }
}
