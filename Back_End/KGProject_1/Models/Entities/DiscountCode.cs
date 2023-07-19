using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class DiscountCode
    {
        public string Id { get; set; }
        public string DiscountCode1 { get; set; }
        public int DiscountRate { get; set; }
        public string DiscountType { get; set; }
    }
}
