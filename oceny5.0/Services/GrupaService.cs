using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;

namespace oceny5._0.Services
{
    public interface IGrupaService
    {
        Task<int> CreateGrupa(CreateGrupaDto dto);
        Task<GrupaDto> GetById(int id);
        Task<IEnumerable<GrupaDto>> GetAll();
        Task Delete(int id); 
        Task Update(int id, GrupaDto dto);

    }

    public class GrupaService : IGrupaService
    {
        private readonly OcenyDBContext _context;
        private readonly IMapper _mapper;

        public GrupaService(OcenyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateGrupa(CreateGrupaDto dto)
        {
            var grupa = _mapper.Map<Grupa>(dto);

            _context.Grupy.Add(grupa);
            await _context.SaveChangesAsync();

            return grupa.Id;
        }

        public async Task<GrupaDto> GetById(int id)
        {
            var result = await _context.Grupy.FirstOrDefaultAsync(c => c.Id == id);
            var resultt = _mapper.Map<GrupaDto>(result);
            return resultt;
        }

        public async Task<IEnumerable<GrupaDto>> GetAll()
        {
            var result = await _context.Grupy.ToListAsync();
            var resultt = _mapper.Map<List<GrupaDto>>(result);
            return resultt;
        }

        public async Task Delete(int id)
        {
            var result = await _context.Grupy.FirstOrDefaultAsync(c => c.Id == id);
            if (result is null)
            {
                throw new NotFoundException($"The grupa with index {id} does not exist");
            }

            _context.Grupy.Remove(result);
            await _context.SaveChangesAsync();

        }

        public async Task Update(int id, GrupaDto dto)
        {
            var grupa = await _context.Grupy.FirstOrDefaultAsync(x => x.Id == id);
            if (grupa is null)
            {
                throw new NotFoundException("Grupa with that ID does not exist");
            }

            grupa.Nazwa = dto.Nazwa;
            _context.Update(grupa);
            await _context.SaveChangesAsync();
        }
        
    }
}
