using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Dtos.AuthDtos;

namespace TaskManagementSystem.Application.Validations
{
    public class RegistrationDtoValidation : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("The Email must Contain @ Sign");
                
        }
    }
}
