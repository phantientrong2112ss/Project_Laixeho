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
    public class TservicesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public TservicesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Tservice>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Tservices
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }

            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Tservices
                .OrderBy(item => item.CreatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Tservices
                .OrderByDescending(item => item.CreatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Tservices.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllTservices")]
        public IActionResult Get()
        {
            var list = this.dbContext.Tservices.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetTservicesbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Tservices.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateTservices")]
        public async Task<IActionResult> CreateNew([FromBody] Tservice data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Tservices_" + randomNumber.ToString();
            dbContext.Tservices.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditTservices/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Tservice data)
        {
            var existingEntity = await dbContext.Tservices.FirstOrDefaultAsync(e => e.Id == id);

    
            if (existingEntity != null)
            {
                existingEntity.IdVehicle = data.IdVehicle;
                existingEntity.Describe = data.Describe;
                existingEntity.Discount = data.Discount;
                existingEntity.Distance = data.Distance;
                existingEntity.TransTime = data.TransTime;
                existingEntity.PriceBDistance = data.PriceBDistance;
                existingEntity.ServicePrice = data.ServicePrice;
                existingEntity.Note = data.Note;
                existingEntity.UpdatedAt = data.UpdatedAt;
           
            await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditTservicesPick/{id}")]
        public async Task<IActionResult> EditPick(string id, [FromBody] Tservice data)
        {
            var existingEntity = await dbContext.Tservices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.PickUpPoint = data.PickUpPoint;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditTservicesDes/{id}")]
        public async Task<IActionResult> EditDes(string id, [FromBody] Tservice data)
        {
            var existingEntity = await dbContext.Tservices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Destination = data.Destination;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteTservices/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Tservices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Tservices.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
