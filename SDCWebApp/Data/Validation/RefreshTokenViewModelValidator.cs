using FluentValidation;
using SDCWebApp.Models.Dtos;

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
