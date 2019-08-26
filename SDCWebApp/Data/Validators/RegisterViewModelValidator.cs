using FluentValidation;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models.Dtos.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace SDCWebApp.Data.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(20)
                .BeValidPassword().WithMessage("{PropertyName} has invalid format.");

            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .BeEmailAddress().WithMessage("{PropertyName} has invalid format.");

            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(2, 20)
                .Matches(new Regex(@"^[a-z]+$")).WithMessage("{PropertyName} can contain only lowercase letters.");

            RuleFor(x => x.Role)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Custom((role, context) =>
                {
                    var dbRoles = dbContext.Roles.ToList();
                    bool isRoleExist = dbRoles.Any(x => x.NormalizedName.Equals(role.ToUpper()));
                    if (isRoleExist == false)
                    {
                        string roles = "";

                        foreach (var dbRole in dbRoles)
                        {
                            roles = $"{roles},{dbRole.NormalizedName}";
                        }
                        context.AddFailure($"{{PropertyName}} '{{PropertyValue}}' doesnt exist. Available roles: {roles}");
                    }
                });
        }
    }
}
