using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using oceny5._0.Models;
using oceny5._0.Services;

namespace oceny5._0.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody]CreateStudentDto dto)
        {
           var result = await _service.Create(dto);
           return Created($"api/student/{result}", null);

        }

        [HttpPost("login")]
        public async Task<ActionResult> GenerateJtwKey([FromBody] LoginStudentDto dto)
        {
            var token = await _service.GenerateJwtKey(dto);
            return Ok(token);
        }

    }
}
