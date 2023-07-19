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
    public class NewsController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public NewsController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<News>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.News
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.News
                .OrderBy(item => item.Title)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.News
                .OrderByDescending(item => item.Title)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.News.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllNews")]
        public IActionResult Get()
        {
            var list = this.dbContext.News.ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("GetNewsbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.News.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateNews")]
        public async Task<IActionResult> CreateNew([FromBody] News data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "News_"+randomNumber.ToString();
            dbContext.News.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditNews/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] News data)
        {
            var existingEntity = await dbContext.News.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Title = data.Title;
                existingEntity.Author = data.Author;
                existingEntity.Content = data.Content;
                existingEntity.Tag = data.Tag;
                existingEntity.CreatedAt = data.CreatedAt;
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteNews/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.News.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.News.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
