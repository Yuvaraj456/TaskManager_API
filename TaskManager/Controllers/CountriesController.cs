using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CountriesController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("[action]")]
        public async Task<List<Country>> GetCountries()
        {
            List<Country> countries = await _db.Countries.OrderBy(x=>x.CountryName).ToListAsync();

            return countries;
        }


        [HttpGet("[action]/{countryId}")]
        public async Task<IActionResult> GetByCountryId([FromRoute] int countryId)
        {
            Country? countries = await _db.Countries.FirstOrDefaultAsync(x => x.CountryId == countryId);

            if(countries != null)
                return Ok(countries);

            return BadRequest($"country not found, for this country Id - {countryId}");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest("Country Object is empty");

            Country? countries = await _db.Countries.FirstOrDefaultAsync(x => x.CountryName == country.CountryName);

            if (countries != null)
                return BadRequest($"country already exist - {country.CountryName}");

            await _db.Countries.AddAsync(country);
            await _db.SaveChangesAsync();

            return Ok(country);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCountry([FromBody] Country country)
        {
            if (country == null)
                return BadRequest("Country Object is empty");

            Country? countries = await _db.Countries.FirstOrDefaultAsync(x => x.CountryId == country.CountryId);

            if (countries == null)
                return BadRequest($"country not exist, unable to update - {country.CountryName}");

            //update
            countries.CountryName = country.CountryName;
           await _db.SaveChangesAsync();

            return Ok(country);
        }

        [HttpDelete("[action]/{countryId}")]
        public async Task<IActionResult> DeleteCountry([FromRoute]int countryId)
        {
            if (countryId == 0)
                return BadRequest("CountryId is empty");

            Country? countries = await _db.Countries.FirstOrDefaultAsync(x => x.CountryId == countryId);

            if (countries == null)
                return BadRequest($"country not exist, unable to delete - {countryId}");

            //delete
            _db.Countries.Remove(countries);
            await _db.SaveChangesAsync();

            return Ok(true);
        }

    }
}
