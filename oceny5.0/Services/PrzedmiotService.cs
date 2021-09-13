using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;

namespace oceny5._0.Services
{
    public interface IPrzedmiotService
    {
        Task<int> Create(CreatePrzedmiotDto dto);
        Task<IEnumerable<PrzedmiotDto>> GetAll();
        Task Delete(int id);
    }

    public class PrzedmiotService : IPrzedmiotService
    {


        private readonly IMapper _mapper;
        private readonly OcenyDBContext _context;

        public PrzedmiotService(IMapper mapper, OcenyDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<int> Create(CreatePrzedmiotDto dto)
        {
            var przedmiot = _mapper.Map<Przedmiot>(dto);
            _context.Przedmioty.Add(przedmiot);
            await _context.SaveChangesAsync();

            return przedmiot.Id;

        }

        public async Task<IEnumerable<PrzedmiotDto>> GetAll()
        {
            var result = await _context.Przedmioty.ToListAsync();

            var resultt = _mapper.Map<List<PrzedmiotDto>>(result);

            return resultt;


        }

        public async Task Delete(int id)
        {
            var result = await _context.Przedmioty.FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
                throw new BadRequestException("Something went wrong");

            _context.Przedmioty.Remove(result);

           await _context.SaveChangesAsync();
        }

    }
}
