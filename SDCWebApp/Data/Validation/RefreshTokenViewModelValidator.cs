using FluentValidation;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class RefreshTokenViewModelValidator : AbstractValidator<RefreshTokenViewModel>, ICustomValidator<RefreshTokenViewModel>
    {
        public RefreshTokenViewModelValidator()
        {
            RuleFor(x => x.AccessToken)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(x => x.RefreshToken)
              .Cascade(CascadeMode.StopOnFirstFailure)
              .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
