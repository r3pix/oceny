using oceny5._0.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0
{
    public class StudentSeeder
    {
        private readonly OcenyDBContext _context;

        public StudentSeeder(OcenyDBContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Studenci.Any())
                {
                    var studenci = GetStudenci();
                    _context.Studenci.AddRange(studenci);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Student> GetStudenci()
        {
            var studenci = new List<Student>()
            {
                new Student()
                {
                    Imie="Piotr",
                    Nazwisko="Jaki",
                    GrupaId=1
                },
                new Student()
                {
                    Imie="Jakub",
                    Nazwisko="Taki",
                    GrupaId=2
                }

            };
            return studenci;
        }

    }
}
