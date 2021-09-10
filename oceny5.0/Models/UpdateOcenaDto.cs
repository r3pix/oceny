using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oceny5._0.Models
{
    public class UpdateOcenaDto
    {
        [Required]
        [Range(2,5,ErrorMessage ="Ocena musi byc od 2 do 5")]
        public int Ocena1 { get; set; }
        public int WykladowcaId { get; set; }


    }
}
