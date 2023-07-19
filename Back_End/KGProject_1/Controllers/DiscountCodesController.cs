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
    public class DiscountCodesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public DiscountCodesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<DiscountCode>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.DiscountCodes
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.DiscountCodes
                .OrderBy(item => item.DiscountRate)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.DiscountCodes
                .OrderByDescending(item => item.DiscountRate)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.DiscountCodes.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllDiscountCodes")]
        public IActionResult Get()
        {
            var list = this.dbContext.DiscountCodes.ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("GetDiscountCodesbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.DiscountCodes.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpGet]
        [Route("Checkdiscount/{id}")]
        public IActionResult GetDiscount(string id)
        {
            var list = dbContext.DiscountCodes.Where(item => item.DiscountCode1 == id);
            return Json(list);
        }


        [HttpPost]
        [Route("CreateDiscountCodes")]
        public async Task<IActionResult> CreateNew([FromBody] DiscountCode data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "DiscountCodes_" + randomNumber.ToString();
            dbContext.DiscountCodes.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditDiscountCodes/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] DiscountCode data)
        {
            var existingEntity = await dbContext.DiscountCodes.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.DiscountCode1 = data.DiscountCode1;
                existingEntity.DiscountRate = data.DiscountRate;
                existingEntity.DiscountType = data.DiscountType;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteDiscountCodes/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.DiscountCodes.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.DiscountCodes.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
