using Microsoft.AspNetCore.Mvc;
using oceny5._0.Entities;
using oceny5._0.Models;
using oceny5._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

//dodawanie zalacznika (plik) swiadectwo, (swagger) 

namespace oceny5._0.Controllers
{
    [Route("api/oceny")]
    [ApiController]
    [Authorize]
    public class OcenaController : ControllerBase
    {

        private readonly IOcenyService _service;

        public OcenaController(IOcenyService service)
        {
            _service = service;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Wykladowca,Admin")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Wykladowca,Admin")]
        public async Task<ActionResult> Update([FromBody]UpdateOcenaDto dto, [FromRoute]int id)
        {
           
            await _service.Update(id, dto);
            return Ok();

        }

        [HttpPost]
        [Authorize(Roles = "Wykladowca,Admin")]
        public async Task<ActionResult> CreateOcena([FromBody] CreateOcenaDto ocenaa)
        {
            var ocena = await _service.CreateOcena(ocenaa);
            return Created($"/api/testocen/{ocena}",null);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OcenaDto>>> GetAll()
        {

            var oceny = await _service.GetAll();
            return Ok(oceny);
        }

        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OcenaDto>>Get([FromRoute]int id)
        {
            var ocena = await _service.GetById(id);
            return ocena;
        }

        [HttpGet("student")]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult<IEnumerable<OcenaDto>>> GetStudentOceny()
        {
            var oceny = await _service.GetStudentOceny();
            return Ok(oceny);
        }
    }
}
