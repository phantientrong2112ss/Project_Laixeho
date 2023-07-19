using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Payment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int AmountM { get; set; }
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
