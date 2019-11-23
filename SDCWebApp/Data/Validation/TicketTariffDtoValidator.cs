using FluentValidation;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class TicketTariffDtoValidator : AbstractValidator<TicketTariffDto>, ICustomValidator<TicketTariffDto>
    {
        public TicketTariffDtoValidator()
        {
            RuleFor(x => x.DefaultPrice)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .GreaterThan(0.0f).WithMessage("{PropertyName} should be greater than 0.")
                    .LessThanOrEqualTo(1000.0f).WithMessage("{PropertyName} should be less than 1000.");

            RuleFor(x => x.Description)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .MaximumLength(300);
        }
    }
}