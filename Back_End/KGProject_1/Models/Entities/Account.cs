using System;
using System.Collections.Generic;

#nullable disable

namespace KGProject_1.Models.Entities
{
    public partial class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public string AccountBalance { get; set; }
        public string UserId { get; set; }
        public string CurrentLocation { get; set; }
        public string Note { get; set; }
        public string Remembertoken { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
