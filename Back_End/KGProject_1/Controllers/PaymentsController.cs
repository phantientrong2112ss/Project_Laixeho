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
    public class PaymentsController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public PaymentsController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Payment>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Payments
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Payments
                .OrderBy(item => item.PaymentStatus)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Payments
                .OrderByDescending(item => item.PaymentStatus)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Payments.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllPayments")]
        public IActionResult Get()
        {
            var list = this.dbContext.Payments.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetPaymentsbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Payments.Where(item => item.Id == id);
            return Json(list);
        }

        [HttpPost]
        [Route("CreatePayments")]
        public async Task<IActionResult> CreateNew([FromBody] Payment data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Payments_" + randomNumber.ToString();
            dbContext.Payments.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditPayments/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Payment data)
        {
            var existingEntity = await dbContext.Payments.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.AmountM = data.AmountM;
                existingEntity.PaymentType = data.PaymentType;
                existingEntity.PaymentStatus = data.PaymentStatus;
                existingEntity.UpdatedAt = data.UpdatedAt; 
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditPaymentStatus/{id}")]
        public async Task<IActionResult> EditPMS(string id, [FromBody] Payment data)
        {
            var existingEntity = await dbContext.Payments.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.AmountM = data.AmountM;
                existingEntity.PaymentStatus = data.PaymentStatus;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeletePayments/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Payments.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Payments.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
