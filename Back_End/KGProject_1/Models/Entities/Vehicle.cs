using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Vehicle
    {
        public string Id { get; set; }
        public string VehiclesName { get; set; }
        public string Cbrand { get; set; }
        public string Illustration { get; set; }
        public string DlicenseRequired { get; set; }
        public string Describe { get; set; }
        public int Sprice { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
