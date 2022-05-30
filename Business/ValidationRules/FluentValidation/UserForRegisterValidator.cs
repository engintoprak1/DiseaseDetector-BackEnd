using Entities.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            RuleFor(u => u.Name).NotEmpty().MaximumLength(100);
            RuleFor(u => u.Surname).NotEmpty().MaximumLength(100);
            RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
            RuleFor(u => u.Gsm).NotEmpty().MaximumLength(11);
        }
    }
}
