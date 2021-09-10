using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace oceny5._0.Models
{
    public class CreateOcenaDto
    {
        public int Ocena1 { get; set; }
        public int PrzedmiotId { get; set; }
        public int StudentId { get; set; }

    }
}
