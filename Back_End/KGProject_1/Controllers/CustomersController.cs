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
    public class CustomersController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public CustomersController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Customer>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Customers
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Customers
                .OrderBy(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Customers
                .OrderByDescending(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Customers.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllCustomers")]
        public IActionResult Get()
        {
            var list = this.dbContext.Customers.ToList();
            return Json(list);
        }

        [HttpGet]
        [Route("GetCustomersbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Customers.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateCustomers")]
        public async Task<IActionResult> CreateNew([FromBody] Customer data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Customers_" + randomNumber.ToString();
            dbContext.Customers.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditCustomers/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Customer data)
        {
            var existingEntity = await dbContext.Customers.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Name = data.Name;
                existingEntity.Sex = data.Sex;
                existingEntity.Email = data.Email;
                existingEntity.Address = data.Address;
                existingEntity.PhoneNumber = data.PhoneNumber;
                existingEntity.Note = data.Note;
                existingEntity.UpdatedAt = data.UpdatedAt;
                
                 await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteCustomers/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Customers.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Customers.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }


        [HttpPost]
        [Route("searchbyname")]
        public IActionResult Getbyname(string searchstring)
        {
            var list = dbContext.Customers.Where(item => (item.Name).Contains(searchstring));
            return Json(new { list });
        }
    }
}
