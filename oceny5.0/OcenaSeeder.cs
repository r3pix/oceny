using oceny5._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0
{
    public class OcenaSeeder
    {
        private readonly OcenyDBContext _context;

        public OcenaSeeder(OcenyDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            { 
                if(!_context.Oceny.Any())
                {
                     var oceny = GetOceny();
                    _context.Oceny.AddRange(oceny);
                    _context.SaveChanges();
                }
               
            }
        }

        private IEnumerable<Ocena> GetOceny()
        {
            var oceny = new List<Ocena>()
            {
                new Ocena()
                {
                    Ocena1=5,
                    StudentId=1,
                    WykladowcaId=1,
                    PrzedmiotId=1

                },
                 new Ocena()
                {
                    Ocena1=4,
                    StudentId=2,
                    WykladowcaId=2,
                    PrzedmiotId=2

                },
                  new Ocena()
                {
                    Ocena1=2,
                    StudentId=1,
                    WykladowcaId=2,
                    PrzedmiotId=2

                },
                   new Ocena()
                {
                    Ocena1=3,
                    StudentId=2,
                    WykladowcaId=1,
                    PrzedmiotId=1

                },



            };
            return oceny;

        }

    }
}
