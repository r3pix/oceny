using oceny5._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0
{
    
    public class GrupaSeeder
    {
        private readonly OcenyDBContext _context;

        public GrupaSeeder (OcenyDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Grupy.Any())
                {
                    var grupy = GetGrupy();
                    _context.Grupy.AddRange(grupy);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Grupa> GetGrupy()
        {
            var grupy = new List<Grupa>()
            {
                new Grupa()
                {
                    Nazwa="Grupa1"
                },
                new Grupa()
                {
                    Nazwa="Grupa2"
                }
            };
            return grupy;

        }


    }
}
