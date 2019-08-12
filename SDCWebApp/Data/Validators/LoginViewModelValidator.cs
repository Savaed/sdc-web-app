using FluentValidation;
using SDCWebApp.Models.ViewModels;
using System.Text.RegularExpressions;

namespace SDCWebApp.Data.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>, ICustomValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Password)
                  .Cascade(CascadeMode.StopOnFirstFailure)
                  .NotEmpty().WithMessage("{PropertyName} is required.")
                  .MaximumLength(20)
                  .BeValidPassword().WithMessage("{PropertyName} has invalid format.");

            RuleFor(x => x.UserName)
               .Cascade(CascadeMode.StopOnFirstFailure)
               .NotEmpty()
               .Length(2, 20)
               .Matches(new Regex(@"^[a-z]+$")).WithMessage("{PropertyName} can contain only lowercase letters.");
        }
    }
}
