using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Infrastructure.DatabaseContext;
using CitiesManager.Core.Models;
using Microsoft.AspNetCore.Cors;

namespace CitiesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    //[EnableCors("4100Client")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cities (including city ID and city name) from 'cities' table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities.OrderBy(temp => temp.CityName).ToListAsync();
            return cities;
        }

        // GET: api/Cities/5
        [HttpGet("{cityID}")]
        public async Task<ActionResult<City>> GetCity(Guid cityID)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityID);

            if (city == null)
            {
                return Problem(detail: "Invalid CityID", statusCode: 400, title: "City Search");
                //return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{cityID}")]
        public async Task<IActionResult> PutCity(Guid cityID, [Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (cityID != city.CityID)
            {
                return BadRequest(); //HTTP 400
            }

            var existingCity = await _context.Cities.FindAsync(cityID);

            if (existingCity == null)
            {
                return NotFound(); //HTTP 404
            }

            existingCity.CityName = city.CityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            //if (ModelState.IsValid == false)
            //{
            //    return ValidationProblem(ModelState);
            //}

            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityID = city.CityID }, city); //api/cities/45c9392a-30f6-42a0-b969-4c97af4f57a8
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            if (_context.Cities == null)
            {
                return NotFound(); //HTTP 404
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound(); //HTTP 404
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }

        private bool CityExists(Guid id)
        {
            return (_context.Cities?.Any(e => e.CityID == id)).GetValueOrDefault();
        }
    }
}
