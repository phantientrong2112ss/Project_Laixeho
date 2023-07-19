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
    public class ComAndRatesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public ComAndRatesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<ComAndRate>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.ComAndRates
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.ComAndRates
                .OrderBy(item => item.CreatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.ComAndRates
                .OrderByDescending(item => item.CreatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.ComAndRates.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllComAndRate")]
        public IActionResult Get()
        {
            var list = this.dbContext.ComAndRates.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetComAndRatebyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.ComAndRates.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetComAndRatebyIdInvoice/{id}")]
        public IActionResult GetCARid(string id)
        {
            var list = dbContext.ComAndRates.Where(item => item.Crid == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateComAndRate")]
        public async Task<IActionResult> CreateNew([FromBody] ComAndRate data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "ComAndRate_" + randomNumber.ToString();
            dbContext.ComAndRates.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditComAndRate/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] ComAndRate data)
        {
            var existingEntity = await dbContext.ComAndRates.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Describe = data.Describe;
                existingEntity.Displaystatus = data.Displaystatus;
             
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteComAndRate/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.ComAndRates.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.ComAndRates.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
