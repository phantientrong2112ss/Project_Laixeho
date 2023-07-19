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
    public class VehiclesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public VehiclesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Vehicle>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Vehicles
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Vehicles
                .OrderBy(item => item.VehiclesName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Vehicles
                .OrderByDescending(item => item.VehiclesName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Vehicles.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllVehicles")]
        public IActionResult Get()
        {
            var list = this.dbContext.Vehicles.ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("GetVehiclesbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Vehicles.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateVehicles")]
        public async Task<IActionResult> CreateNew([FromBody] Vehicle data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Vehicles_" + randomNumber.ToString();
            dbContext.Vehicles.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditVehicles/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Vehicle data)
        {
            var existingEntity = await dbContext.Vehicles.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.VehiclesName = data.VehiclesName;
                existingEntity.Cbrand = data.Cbrand;
                existingEntity.Illustration = data.Illustration;
                existingEntity.DlicenseRequired = data.DlicenseRequired;
                existingEntity.Describe = data.Describe;
                existingEntity.Sprice = data.Sprice;
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });

        }

        [HttpDelete]
            [Route("DeleteVehicles/{id}")]
            public async Task<IActionResult> Delete(string id)
            {
                var existingEntity = await dbContext.Vehicles.FirstOrDefaultAsync(e => e.Id == id);

                if (existingEntity != null)
                {
                    dbContext.Vehicles.Remove(existingEntity);
                    await dbContext.SaveChangesAsync();
                }
                return Json(new { existingEntity });
            }

        [HttpPost]
        [Route("searchbyname")]
        public IActionResult Getbyname(string searchstring)
        {
            var list = dbContext.Vehicles.Where(item => (item.VehiclesName).Contains(searchstring));
            return Json(new { list });
        }
    }

    } 
