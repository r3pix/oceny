using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using oceny5._0.Entities;

namespace oceny5._0.Models.Validation
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        

        public CreateStudentDtoValidator(OcenyDBContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(4);

            RuleFor(x => x.ConfirmPassword).Equal(c => c.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
                {
                    if (dbContext.Studenci.Any(c => c.Email == value))
                    {
                        context.AddFailure("Student with this email alredy exists");
                    }
                }
            );
        }
    }
}
