using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Tservice
    {
        public string Id { get; set; }
        public string IdVehicle { get; set; }
        public string Describe { get; set; }
        public string Discount { get; set; }
        public string PickUpPoint { get; set; }
        public string Destination { get; set; }
        public string Distance { get; set; }
        public string TransTime { get; set; }
        public int PriceBDistance { get; set; }
        public int ServicePrice { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
