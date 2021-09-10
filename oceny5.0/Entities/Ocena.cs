using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Ocena
    {
        public int Id { get; set; }
        public double Ocena1 { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }


        public int WykladowcaId { get; set; }
        public virtual Wykladowca Wykladowca { get; set; }

        public int PrzedmiotId { get; set; }
        public virtual Przedmiot Przedmiot { get; set; }
        
    }
}
