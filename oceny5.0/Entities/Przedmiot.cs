using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Przedmiot
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public virtual List<Ocena> Oceny { get; set; }
    }
}
