using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public int GrupaId { get; set; }
        public virtual Grupa Grupa { get; set; }
        public virtual List<Ocena> Oceny { get; set; }
        public string Role { get; set; } = "Student";
    }
}
