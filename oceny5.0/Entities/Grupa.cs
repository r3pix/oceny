using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace oceny5._0.Entities
{
    public class Grupa
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public List<Student> Studenci { get; set; }
    }
}
