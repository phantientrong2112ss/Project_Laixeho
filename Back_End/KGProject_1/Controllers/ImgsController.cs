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
    public class ImgsController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public ImgsController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Img>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Imgs
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Imgs
                .OrderBy(item => item.Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Imgs
                .OrderByDescending(item => item.Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Imgs.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllImgs")]
        public IActionResult Get()
        {
            var list = this.dbContext.Imgs.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetImgsbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Imgs.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateImgs")]
        public async Task<IActionResult> CreateNew([FromBody] Img data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Imgs_" + randomNumber.ToString();
            dbContext.Imgs.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditImgs/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Img data)
        {
            var existingEntity = await dbContext.Imgs.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {

                existingEntity.Tid = data.Tid;
                existingEntity.Urlimg = data.Urlimg;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteImgs/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Imgs.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Imgs.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
