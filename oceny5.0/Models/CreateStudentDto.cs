using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Models
{
    public class CreateStudentDto
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int GrupaId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
