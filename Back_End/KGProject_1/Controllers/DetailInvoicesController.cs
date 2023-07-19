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
    public class DetailInvoicesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public DetailInvoicesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<DetailInvoice>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.DetailInvoices
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.DetailInvoices
                .OrderBy(item => item.Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.DetailInvoices
                .OrderByDescending(item => item.Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.DetailInvoices.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllDetailInvoices")]
        public IActionResult Get()
        {
            var list = this.dbContext.DetailInvoices.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetDetailInvoicesbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.DetailInvoices.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetDetailInvoicesbyInvoiceId/{id}")]
        public async Task<IActionResult> GetbyInvoiceid(string id)
        {
            var list = await dbContext.DetailInvoices
                .Join(dbContext.Tservices, t1 => t1.IdService, t2 => t2.Id, (t1, t2) => new { t1, t2 })
                .Join(dbContext.Vehicles, t => t.t2.IdVehicle, t3 => t3.Id, (t, t3) => new {
                    DetailInvoice = t.t1,
                    Service = t.t2,
                    Vehicle=t3
                })
                .Where(item => item.DetailInvoice.IdInvoice == id).ToListAsync();
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateDetailInvoices")]
        public async Task<IActionResult> CreateNew([FromBody] DetailInvoice data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "DetailInvoices_" + randomNumber.ToString();
            dbContext.DetailInvoices.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });

        }

        [HttpPut]
        [Route("EditDetailInvoices/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] DetailInvoice data)
        {
            var existingEntity = await dbContext.DetailInvoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.UpdatedAt = data.UpdatedAt;
                  await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteDetailInvoices/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.DetailInvoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.DetailInvoices.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
