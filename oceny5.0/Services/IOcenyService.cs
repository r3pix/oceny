using oceny5._0.Entities;
using oceny5._0.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace oceny5._0.Services
{
    public interface IOcenyService
    {
        Task<int> CreateOcena(CreateOcenaDto ocenaa);
        Task<IEnumerable<OcenaDto>> GetAll();
        Task<OcenaDto> GetById(int id);
        Task Delete(int id);
        Task Update(int id, UpdateOcenaDto dto);
        Task<IEnumerable<OcenaDto>> GetStudentOceny();
    }
}