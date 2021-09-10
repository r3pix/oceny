using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using oceny5._0.Entities;
using oceny5._0.Models;

namespace oceny5._0
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateOcenaDto, Ocena>()
                .ForMember(c => c.Ocena1, m => m.MapFrom(d => d.Ocena1))
                .ForMember(c => c.PrzedmiotId, m => m.MapFrom(d => d.PrzedmiotId))
                .ForMember(c => c.StudentId, m => m.MapFrom(d => d.StudentId));

            CreateMap<Ocena, OcenaDto>()
                .ForMember(c => c.Ocena1, m => m.MapFrom(d => d.Ocena1))
                .ForMember(c => c.PrzedmiotId, m => m.MapFrom(d => d.PrzedmiotId))
                .ForMember(c=>c.WykladowcaId,m=>m.MapFrom(d=>d.WykladowcaId))
                .ForMember(c=>c.StudentId, m=>m.MapFrom(d=>d.StudentId));

            CreateMap<CreateGrupaDto, Grupa>()
                .ForMember(c => c.Nazwa, m => m.MapFrom(d=>d.Nazwa));

            CreateMap<Grupa, GrupaDto>()
                .ForMember(c => c.Nazwa, m => m.MapFrom(d => d.Nazwa));

            CreateMap<CreateWykladowcaDto, Wykladowca>()
                .ForMember(x => x.Email, m => m.MapFrom(c => c.Email))
                .ForMember(x => x.HashedPassword, m => m.MapFrom(c => c.Password))
                .ForMember(x => x.Imie, m => m.MapFrom(c => c.Imie))
                .ForMember(x => x.Nazwisko, m => m.MapFrom(c => c.Nazwisko))
                .ForMember(x => x.Stopien, m => m.MapFrom(c => c.Stopien));

            CreateMap<CreateStudentDto,Student>()
                .ForMember(x=>x.Email, m=>m.MapFrom(c=>c.Email))
                .ForMember(x=>x.GrupaId,m=>m.MapFrom(c=>c.GrupaId))
                .ForMember(x=>x.Imie, m=>m.MapFrom(c=>c.Imie))
                .ForMember(x=>x.Nazwisko, m=>m.MapFrom(c=>c.Nazwisko));
                

        }
    }
}
