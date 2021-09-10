using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using oceny5._0.Entities;

namespace oceny5._0.Models
{
    public class CreateWykladowcaDtoValidator : AbstractValidator<CreateWykladowcaDto>
    {
        

        public CreateWykladowcaDtoValidator(OcenyDBContext dbContext)
        {
            
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Password)
                .MinimumLength(4);

            RuleFor(c => c.ConfirmPassword).Equal(d => d.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                    {
                        if (dbContext.Wykladowcy.Any(d => d.Email == value))
                        {
                            context.AddFailure("This email is already in use for entity wykladowcy");
                        }
                    }
                );
        }
    }
}
