using oceny5._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0
{
    public class PrzedmiotSeeder
    {
        private readonly OcenyDBContext _context;

        public PrzedmiotSeeder(OcenyDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Przedmioty.Any())
                {
                    var przedmioty = GetPrzedmioty();
                    _context.Przedmioty.AddRange(przedmioty);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Przedmiot> GetPrzedmioty()
        {
            var przedmioty = new List<Przedmiot>()
            {
                new Przedmiot()
                {
                    Nazwa="Historia"
                },
                new Przedmiot()
                {
                    Nazwa="Matematyka"
                }
            };
            return przedmioty;
        }
        


    }
}
