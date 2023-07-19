using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Invoice
    {
        public string Id { get; set; }
        public string IdCustomer { get; set; }
        public string IdDriver { get; set; }
        public int TotalAmount { get; set; }
        public string Paymentid { get; set; }
        public string InvoiceStatus { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
