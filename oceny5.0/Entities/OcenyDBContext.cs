using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class OcenyDBContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\LocalDB;Database=OcenyDb;" +
            "Trusted_Connection=True;";

        public DbSet<Grupa> Grupy { get; set; }
        public DbSet<Ocena> Oceny { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Student> Studenci { get; set; }
        public DbSet<Wykladowca> Wykladowcy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
