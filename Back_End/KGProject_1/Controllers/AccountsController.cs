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
    public class AccountsController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public AccountsController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            var sortpagedData = new List<Account>();
            if (filter.Sortstring == "none")
            {
                sortpagedData = await dbContext.Accounts
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            }
            else if (filter.Sortstring == "sortnameas")
            {
                sortpagedData = await dbContext.Accounts
                .OrderBy(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                sortpagedData = await dbContext.Accounts
                .OrderByDescending(item => item.Name)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            }
            var totalRecords = await dbContext.Accounts.CountAsync();
            return Json(new { sortpagedData, totalRecords });
        }


        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult Get()
        {
            var list = this.dbContext.Accounts.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetAccountsbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Accounts.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpPost]
        [Route("CreateAccounts")]
        public async Task<IActionResult> CreateNew([FromBody] Account data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Accounts_" + randomNumber.ToString();
            dbContext.Accounts.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditAccounts/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Account data)
        {
            var existingEntity = await dbContext.Accounts.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Name = data.Name;
                existingEntity.Username = data.Username;
                existingEntity.Email = data.Email;
                existingEntity.PhoneNumber = data.PhoneNumber;
                existingEntity.Password = data.Password;
                existingEntity.Image = data.Image;
                existingEntity.Role = data.Role;
                existingEntity.AccountBalance = data.AccountBalance;
                existingEntity.Remembertoken = data.Remembertoken;
                existingEntity.Note = data.Note;
                existingEntity.CurrentLocation = data.CurrentLocation;
                existingEntity.UpdatedAt = data.UpdatedAt;
        await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditPassword/{id}")]
        public async Task<IActionResult> EditPassword(string id, [FromBody] Account data)
        {
            var existingEntity = await dbContext.Accounts.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.Name = data.Name;
                existingEntity.Email = data.Email;
                existingEntity.PhoneNumber = data.PhoneNumber;
                existingEntity.Password = data.Password;
                existingEntity.CurrentLocation = data.CurrentLocation;
                existingEntity.UpdatedAt = data.UpdatedAt;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpDelete]
        [Route("DeleteAccounts/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Accounts.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Accounts.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }

        [HttpPost]
        [Route("searchbyname")]
        public IActionResult Getbyname(string searchstring)
        {
            var list = dbContext.Accounts.Where(item => (item.Name).Contains(searchstring));
            return Json(new { list });
        }
    }
}
