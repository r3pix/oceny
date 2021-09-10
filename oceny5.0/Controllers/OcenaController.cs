using Microsoft.AspNetCore.Mvc;
using oceny5._0.Entities;
using oceny5._0.Models;
using oceny5._0.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//dodawanie zalacznika (plik) swiadectwo, (swagger) 

namespace oceny5._0.Controllers
{
    [Route("api/oceny")]
    [ApiController]
    public class OcenaController : ControllerBase
    {

        private readonly IOcenyService _service;

        public OcenaController(IOcenyService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody]UpdateOcenaDto dto, [FromRoute]int id)
        {
            /*
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            await _service.Update(id, dto);
            return Ok();

        }

        [HttpPost]
        public async Task<ActionResult> CreateOcena([FromBody] CreateOcenaDto ocenaa )
        {
            var ocena = await _service.CreateOcena(ocenaa);
            return Created($"/api/testocen/{ocena}",null);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OcenaDto>>> GetAll()
        {

            var oceny = await _service.GetAll();
            return Ok(oceny);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OcenaDto> >Get([FromRoute]int id)
        {
            var ocena = await _service.GetById(id);
            return ocena;
        }
    }
}
