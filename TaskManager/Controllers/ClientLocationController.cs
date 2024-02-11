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


        [HttpGet("[action]")]
        public async Task<ActionResult<List<ClientLocation>>> Get()
        {

            List<ClientLocation> locations = await _db.ClientLocations.ToListAsync();
            return locations;
        }



        [HttpGet("[action]/{clientLocationId}")]
        public async Task<IActionResult> GetByClientLocationId([FromRoute] int clientLocationId)
        {
            ClientLocation? clientLocation = await _db.ClientLocations.FirstOrDefaultAsync(x => x.ClientLocationId == clientLocationId);

            if (clientLocation != null)
                return Ok(clientLocation);

            return BadRequest($"country not found, for this country Id - {clientLocationId}");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddClientLocation([FromBody] ClientLocation clientLocation)
        {
            if (clientLocation == null)
                return BadRequest("ClientLocation Object is empty");

            ClientLocation? location = await _db.ClientLocations.FirstOrDefaultAsync(x => x.ClientLocationName == clientLocation.ClientLocationName);

            if (location != null)
                return BadRequest($"ClientLocation already exist - {clientLocation.ClientLocationName}");

            await _db.ClientLocations.AddAsync(clientLocation);
            await _db.SaveChangesAsync();

            return Ok(clientLocation);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateClientLocation([FromBody] ClientLocation clientLocation)
        {
            if (clientLocation == null)
                return BadRequest("ClientLocation Object is empty");

            ClientLocation? location = await _db.ClientLocations.FirstOrDefaultAsync(x => x.ClientLocationId == clientLocation.ClientLocationId);

            if (location == null)
                return BadRequest($"ClientLocation not exist, unable to update - {clientLocation.ClientLocationName}");

            //update
            location.ClientLocationName = clientLocation.ClientLocationName;
            await _db.SaveChangesAsync();

            return Ok(clientLocation);
        }

        [HttpDelete("[action]/{clientLocationId}")]
        public async Task<IActionResult> DeleteClientLocation([FromRoute] int clientLocationId)
        {
            if (clientLocationId == 0)
                return BadRequest("ClientLocationId is empty");

            ClientLocation? clientLocation = await _db.ClientLocations.FirstOrDefaultAsync(x => x.ClientLocationId == clientLocationId);

            if (clientLocation == null)
                return BadRequest($"ClientLocation not exist, unable to delete - {clientLocationId}");

            //delete
            _db.ClientLocations.Remove(clientLocation);
            await _db.SaveChangesAsync();

            return Ok(true);
        }
    }
}
