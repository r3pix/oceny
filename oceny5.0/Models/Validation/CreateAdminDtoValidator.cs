using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using oceny5._0.Entities;

namespace oceny5._0.Models.Validation
{
    public class CreateAdminDtoValidator : AbstractValidator<CreateAdminDto>
    {
        public CreateAdminDtoValidator(OcenyDBContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(4);
            RuleFor(x => x.ConfirmPassword).Equal(c => c.Password);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                if (dbContext.Admins.Any(b => b.Email == value))
                {
                    context.AddFailure("Admin with this email already exists");
                }
            });
        }

    }
}
