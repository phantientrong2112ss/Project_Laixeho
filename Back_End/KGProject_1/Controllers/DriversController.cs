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
    public class DriversController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public DriversController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Driver>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Drivers
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Drivers
                .OrderBy(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Drivers
                .OrderByDescending(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Drivers.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllDrivers")]
        public IActionResult Get()
        {
            var list = this.dbContext.Drivers.ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("GetDriversbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Drivers.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateDrivers")]
        public async Task<IActionResult> CreateNew([FromBody] Driver data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Drivers_" + randomNumber.ToString();
            dbContext.Drivers.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditDrivers/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Driver data)
        {
            var existingEntity = await dbContext.Drivers.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Name = data.Name;
                existingEntity.Sex = data.Sex;
                existingEntity.Birthday = data.Birthday;
                existingEntity.Address = data.Address;
                existingEntity.PhoneNumber = data.PhoneNumber;
                existingEntity.Email = data.Email;
                existingEntity.DriverLicense = data.DriverLicense;
                existingEntity.Rate = data.Rate;
                existingEntity.Note = data.Note;
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditDriverStatus/{id}")]
        public async Task<IActionResult> EditStatus(string id, [FromBody] Driver data)
        {
            var existingEntity = await dbContext.Drivers.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Note = data.Note;
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteDrivers/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Drivers.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Drivers.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }

        [HttpPost]
        [Route("searchbyname")]
        public IActionResult Getbyname(string searchstring)
        {
            var list = dbContext.Drivers.Where(item => (item.Name).Contains(searchstring));
            return Json(new { list });
        }
    }
}
