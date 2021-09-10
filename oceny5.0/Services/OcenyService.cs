using Microsoft.Extensions.Logging;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace oceny5._0.Services
{
    public class OcenyService : IOcenyService
    {
        private readonly OcenyDBContext _context;
        private readonly ILogger<OcenyService> _logger;
        private readonly IMapper _mapper;

        public OcenyService(OcenyDBContext context, ILogger<OcenyService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OcenaDto> GetById(int id)
        {
            _logger.LogError($"Ocena with id: {id} was displayed");
            var ocena = await _context.Oceny.FirstOrDefaultAsync(r => r.Id == id);
            if (ocena is null)
            {
                throw new NotFoundException("Ocena not found");
            }

            var ocenaa = _mapper.Map<OcenaDto>(ocena);
            return ocenaa;
        }

        public async Task<IEnumerable<OcenaDto>> GetAll()
        {
            var oceny = await _context.Oceny.ToListAsync();
            var ocenyy = _mapper.Map<List<OcenaDto>>(oceny);
            return ocenyy;
        }

        public async Task Update(int id, UpdateOcenaDto dto)
        {
            var ocena = await _context.Oceny.FirstOrDefaultAsync(r => r.Id == id);
            if(ocena is null)
            {
                throw new NotFoundException("Ocena not found");
            }

            ocena.Ocena1 = dto.Ocena1;
            ocena.WykladowcaId = dto.WykladowcaId;

            _context.Oceny.Update(ocena);
            await _context.SaveChangesAsync();

      
        }

        public async Task<int> CreateOcena(CreateOcenaDto dto)
        {
             var ocenaa = _mapper.Map<Ocena>(dto);
             _context.Oceny.Add(ocenaa);
             await _context.SaveChangesAsync();

            return ocenaa.Id;

        }

        public async Task Delete(int id)
        {
            var result = await _context.Oceny.FirstOrDefaultAsync(r => r.Id == id);

            if(result is null)
            {
                throw new NotFoundException("Ocena not found");
            }
            _context.Oceny.Remove(result);
            await _context.SaveChangesAsync();
            
        }


    }
}
