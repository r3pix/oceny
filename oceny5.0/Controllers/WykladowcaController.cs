using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using oceny5._0.Models;
using oceny5._0.Services;

namespace oceny5._0.Controllers
{
    [Route("api/wykladowca")]
    [ApiController]
    public class WykladowcaController : ControllerBase
    {
        private readonly IWykladowcaService _wykladowcaService;

        public WykladowcaController(IWykladowcaService wykladowcaService)
        {
            _wykladowcaService = wykladowcaService;
        }


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateWykladowcaDto dto)
        {
            var result = await _wykladowcaService.Create(dto);
            return Created($"/api/wykladowca/{result}",null);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]LoginWykladowcaDto dto)
        {
            string token = await _wykladowcaService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
