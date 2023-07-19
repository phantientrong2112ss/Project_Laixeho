using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Driver
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DriverLicense { get; set; }
        public string Rate { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
