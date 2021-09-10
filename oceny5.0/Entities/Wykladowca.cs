using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Wykladowca
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stopien { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public virtual List<Ocena> Oceny { get; set; }
        public string Role { get; set; } = "Wykladowca";

    }
}
