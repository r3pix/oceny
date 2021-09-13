using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using FluentValidation;
using oceny5._0.Entities;

namespace oceny5._0.Models.Validation
{
    public class CreatePrzedmiotDtoValidator : AbstractValidator<CreatePrzedmiotDto>
    {
        public CreatePrzedmiotDtoValidator(OcenyDBContext dbContext)
        {
            RuleFor(x => x.Nazwa).NotEmpty();
            RuleFor(x => x.Nazwa).Custom((value, context) =>
            {
                if (dbContext.Przedmioty.Any(c => c.Nazwa == value))
                {
                    context.AddFailure("Przedmiot called like that already exists");
                }
            });
        }

    }
}
