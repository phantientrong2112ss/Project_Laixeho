using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class ComAndRate
    {
        public string Id { get; set; }
        public string Crid { get; set; }
        public string UserId { get; set; }
        public string Describe { get; set; }
        public string Displaystatus { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
