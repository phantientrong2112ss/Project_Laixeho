using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KGProject_1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KGProject_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public LocationsController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Location>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Locations
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Locations
                .OrderBy(item => item.LocationName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Locations
                .OrderByDescending(item => item.LocationName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Locations.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllLocations")]
        public IActionResult Get()
        {
            var list = this.dbContext.Locations.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetLocationsbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Locations.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateLocations")]
        public async Task<IActionResult> CreateNew([FromBody] Location data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Locations_" + randomNumber.ToString();
            dbContext.Locations.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditLocations/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Location data)
        {
            var existingEntity = await dbContext.Locations.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.LocationName = data.LocationName;
                existingEntity.Address = data.Address;
                existingEntity.Coordinates = data.Coordinates;
                existingEntity.Describe = data.Describe;
                existingEntity.Locationp1 = data.Locationp1;
                existingEntity.Locationp2 = data.Locationp2;
                existingEntity.Locationp3 = data.Locationp3;
               
        await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteLocations/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Locations.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Locations.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
