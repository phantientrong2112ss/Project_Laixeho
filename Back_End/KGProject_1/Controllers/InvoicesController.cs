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
    public class InvoicesController : Controller
    {
        private readonly GPChauffeurContext dbContext;

        public InvoicesController(GPChauffeurContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("pagination")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.Sortstring);
            if (filter.Sortstring == "none")
            {
                var sortpagedData = await dbContext.Invoices
                 .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
                 .Join(dbContext.Drivers, t => t.t1.IdDriver, t3 => t3.Id, (t, t3) => new { t, t3 })
                 .Join(dbContext.Payments, s => s.t.t1.Paymentid, t4 => t4.Id, (s, t4) => new
                 {
                     hoadon = s.t.t1,
                     khachhang = s.t.t2.Name,
                     taixe = s.t3.Name,
                     payment=t4.PaymentType
                 })
               .Where(i => i.hoadon.TotalAmount != 0)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
                var totalRecords = await dbContext.Invoices.CountAsync();
                return Json(new { sortpagedData, totalRecords });
            }
            else if (filter.Sortstring == "nodriver")
            {
                var sortpagedData = await dbContext.Invoices
                 .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
                 .Join(dbContext.Payments, s => s.t1.Paymentid, t3 => t3.Id, (s, t3) => new
                 {
                     hoadon = s.t1,
                     khachhang = s.t2.Name,
                     payment = t3.PaymentType
                 })
               .Where(t=>t.hoadon.IdDriver == "" || t.hoadon.TotalAmount == 0)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
                var totalRecords = await dbContext.Invoices.CountAsync();
                return Json(new { sortpagedData, totalRecords });
            }
            //.Join(dbContext.Payments, s => s.t.t1.Paymentid, t4 => t4.Id, (s, t4) => new { s, t4 })
            else if (filter.Sortstring == "sortnameas")
            {
                var sortpagedData = await dbContext.Invoices
                .OrderBy(item => item.InvoiceStatus)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
                var totalRecords = await dbContext.Invoices.CountAsync();
                return Json(new { sortpagedData, totalRecords });
            }
            else if (filter.Sortstring == "sortnamedes")
            {
                var sortpagedData = await dbContext.Invoices
                .OrderByDescending(item => item.InvoiceStatus)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
                var totalRecords = await dbContext.Invoices.CountAsync();
                return Json(new { sortpagedData, totalRecords });
            }
            return Json(new { });
        }


        [HttpGet]
        [Route("GetAllInvoices")]
        public IActionResult Get()
        {
            var list = this.dbContext.Invoices.ToList();
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetInvoicesbyId/{id}")]
        public IActionResult Getbyid(string id)
        {
            var list = dbContext.Invoices.Where(item => item.Id == id);
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetInvoicesbyIdCustomer/{id}")]
        public IActionResult GetbyCusid(string id)
        {
            var list = dbContext.Invoices
                .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
                .Join(dbContext.Payments, s => s.t1.Paymentid, t3 => t3.Id, (s, t3) => new
                {
                    hoadon = s.t1,
                    khachhang = s.t2.Name,
                    payment = t3
                }).Where(t => t.hoadon.IdCustomer == id);
            return Json(new { list });
        }

        [HttpGet]
        [Route("GetInvoicesbyIdDriver/{id}")]
        public IActionResult GetbyDriverid(string id)
        {
            var list = dbContext.Invoices
                .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
                .Join(dbContext.Payments, s => s.t1.Paymentid, t3 => t3.Id, (s, t3) => new
                {
                    hoadon = s.t1,
                    khachhang = s.t2.Name,
                    payment = t3
                }).Where(t => t.hoadon.IdDriver == id);
            return Json(new { list });
        }


        [HttpGet]
        [Route("GetInvoicesSuitWithDriver/{id}")]
        public IActionResult GetInvoiceSuitDriver(string id)
        {

            List<string> checkDriverlicense = new List<string>();
            string[] listdl = id.Split(',');
            foreach(string item in listdl)
            {
                checkDriverlicense.Add(item);
            }
            //var list = dbContext.Invoices
            //    .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
            //    .Join(dbContext.Payments, s => s.t1.Paymentid, t3 => t3.Id, (s, t3) => new
            //    {
            //        hoadon = s.t1,
            //        khachhang = s.t2.Name,
            //        payment = t3
            //    }).Where(t => t.hoadon.IdDriver == id);
            var listS = dbContext.Invoices
               .Join(dbContext.Customers, t1 => t1.IdCustomer, t2 => t2.Id, (t1, t2) => new { t1, t2 })
               .Join(dbContext.Payments, s => s.t1.Paymentid, t3 => t3.Id, (s, t3) => new { s, t3 })
               .Join(dbContext.DetailInvoices, d => d.s.t1.Id, t4 => t4.IdInvoice, (d, t4) => new { d, t4 })
               .Join(dbContext.Tservices, f => f.t4.IdService, t5 => t5.Id, (f, t5) => new { f, t5 })
               .Join(dbContext.Vehicles, e => e.t5.IdVehicle, t6 => t6.Id, (e, t6) => new {e,t6})
               .Join(dbContext.Locations, r => r.e.t5.PickUpPoint, t7 => t7.Id, (r, t7) => new
               {
                   hoadon = r.e.f.d.s.t1,
                   khachhang = r.e.f.d.s.t2,
                   payment = r.e.f.d.t3,
                   vehicle = r.t6,
                   service = r.e.t5,
                   detailinvoice = r.e.f.t4,
                   pickuplocation=t7
               })
               .Where(t => checkDriverlicense.Contains(t.vehicle.DlicenseRequired) && t.hoadon.IdDriver=="" && t.hoadon.TotalAmount>0);
            return Json(new { listS });
        }


        [HttpPost]
        [Route("CreateInvoices")]
        public async Task<IActionResult> CreateNew([FromBody] Invoice data)
        {
      
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            data.Id = "Invoices_" + randomNumber.ToString();
            dbContext.Invoices.Add(data);
            await dbContext.SaveChangesAsync();
            return Json(data.Id);

        }

        [HttpPut]
        [Route("EditInvoices/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Invoice data)
        {
            var existingEntity = await dbContext.Invoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.IdCustomer = data.IdCustomer;
                existingEntity.IdDriver = data.IdDriver;
                existingEntity.TotalAmount = data.TotalAmount;
                existingEntity.Paymentid = data.Paymentid;
                existingEntity.InvoiceStatus = data.InvoiceStatus;
                existingEntity.Note = data.Note;

                existingEntity.UpdatedAt = data.UpdatedAt;

                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditInvoicesStatus/{id}")]
        public async Task<IActionResult> EditIS(string id, [FromBody] Invoice data)
        {
            var existingEntity = await dbContext.Invoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.InvoiceStatus = data.InvoiceStatus;
                existingEntity.UpdatedAt = data.UpdatedAt;

                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("EditInvoicesDriver/{id}")]
        public async Task<IActionResult> EditDriverofInvoice(string id, [FromBody] Invoice data)
        {
            var existingEntity = await dbContext.Invoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.IdDriver = data.IdDriver;
                existingEntity.InvoiceStatus = data.InvoiceStatus;
                existingEntity.UpdatedAt = data.UpdatedAt;

                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }

        [HttpPut]
        [Route("ChangeTotalAmountInvoices/{id}")]
        public async Task<IActionResult> changeTTAmount(string id, [FromBody] int data)
        {
            var existingEntity = await dbContext.Invoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                existingEntity.TotalAmount = data;
                await dbContext.SaveChangesAsync();
            }
            return Json(new { data });
        }


        [HttpDelete]
        [Route("DeleteInvoices/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEntity = await dbContext.Invoices.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEntity != null)
            {
                dbContext.Invoices.Remove(existingEntity);
                await dbContext.SaveChangesAsync();
            }
            return Json(new { existingEntity });
        }
    }
}
