using oceny5._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0
{
    public class WykladowcaSeeder
    {
        private readonly OcenyDBContext _context;

        public WykladowcaSeeder(OcenyDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Wykladowcy.Any())
                {
                    var wykladowcy = GetWykladowcy();
                    _context.Wykladowcy.AddRange(wykladowcy);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Wykladowca> GetWykladowcy()
        {
            var wykladowcy = new List<Wykladowca>()
            {
                new Wykladowca()
                {
                    Imie = "Krzysztof",
                    Nazwisko = "Jarecki",
                    Stopien = "dr"
                },
                new Wykladowca()
                {
                    Imie="Jan",
                    Nazwisko="Krzeszowski",
                    Stopien = "inz"
                }

            };
            return wykladowcy;
        }


    }
}
