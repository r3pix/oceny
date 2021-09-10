using Microsoft.Extensions.Logging;
using oceny5._0.Entities;
using oceny5._0.Exceptions;
using oceny5._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using oceny5._0.Authorization;

namespace oceny5._0.Services
{
    public class OcenyService : IOcenyService
    {
        private readonly OcenyDBContext _context;
        private readonly ILogger<OcenyService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public OcenyService(OcenyDBContext context, ILogger<OcenyService> logger, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
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

            var result = _authorizationService.AuthorizeAsync(_userContextService.User, ocena,
                new ManageOcenaRequirement(Operation.Update)).Result;

            if (!result.Succeeded)
            {
                throw new BadRequestException("You can not modify this ocena");
            }

            ocena.Ocena1 = dto.Ocena1;

            _context.Oceny.Update(ocena);
            await _context.SaveChangesAsync();

      
        }

        public async Task<int> CreateOcena(CreateOcenaDto dto)
        {
             var ocenaa = _mapper.Map<Ocena>(dto);
             ocenaa.WykladowcaId = (int)_userContextService.GetUserId;
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

            var authenticationResult = _authorizationService.AuthorizeAsync(_userContextService.User, result,
                new ManageOcenaRequirement(Operation.Delete)).Result;

            if (!authenticationResult.Succeeded)
            {
                throw new BadRequestException("you can not modify this ocena");
            }
            _context.Oceny.Remove(result);
            await _context.SaveChangesAsync();
            
        }


    }
}
