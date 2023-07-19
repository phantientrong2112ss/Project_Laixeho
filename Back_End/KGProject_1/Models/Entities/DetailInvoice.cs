using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class DetailInvoice
    {
        public string Id { get; set; }
        public string IdInvoice { get; set; }
        public string IdService { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
