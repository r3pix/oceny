using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; } = "Admin";
    }
}
