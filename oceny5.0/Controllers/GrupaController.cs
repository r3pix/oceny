using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using oceny5._0.Models;
using oceny5._0.Services;

namespace oceny5._0.Controllers
{
    [Route("api/grupa")]
    [ApiController]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _service;

        public GrupaController(IGrupaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> CreateGrupa([FromBody]CreateGrupaDto dto)
        {
            var result = await _service.CreateGrupa(dto);
            return Created($"/api/grupa/{result}",null);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupaDto>> GetById([FromRoute] int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupaDto>>> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] GrupaDto dto)
        {
            await _service.Update(id,dto);
            return Ok();
        }

        
    }
}
