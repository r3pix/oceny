using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using oceny5._0.Models;
using oceny5._0.Services;

namespace oceny5._0.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody] CreateAdminDto dto)
        {
            var result=await _adminService.Create(dto);
            return Created($"api/admin/{result}", null);
        }

        [HttpPost("login")]
        public async Task<ActionResult> GenerateJwtToken([FromBody]LoginAdminDto dto)
        {
            var token = await _adminService.GenerateJwtToken(dto);
            return Ok(token);

        }

    }
}
