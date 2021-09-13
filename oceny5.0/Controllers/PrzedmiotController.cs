using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using oceny5._0.Entities;
using oceny5._0.Models;
using oceny5._0.Services;

namespace oceny5._0.Controllers
{
    [Route("api/przedmiot")]
    [ApiController]
    public class PrzedmiotController : ControllerBase
    {
        private readonly IPrzedmiotService _service;

        public PrzedmiotController(IPrzedmiotService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatePrzedmiotDto dto)
        {
            var result= await _service.Create(dto);
            return Created($"/api/przedmiot{result}", null);

        }

        [HttpGet]
        public async Task<IEnumerable<PrzedmiotDto>> GetAll()
        {
            var result = await _service.GetAll();
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
