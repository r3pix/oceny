using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Models
{
    public class CreateWykladowcaDto
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stopien { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public string Password { get; set; }

    }
}
