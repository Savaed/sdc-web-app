using FluentValidation;
using SDCWebApp.Models;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingTariffValidator : AbstractValidator<SightseeingTariff>, ICustomValidator<SightseeingTariff>
    {
        public SightseeingTariffValidator()
        {
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleForEach(x => x.TicketTariffs)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SetValidator(new TicketTariffValidator());
        }
    }
}
