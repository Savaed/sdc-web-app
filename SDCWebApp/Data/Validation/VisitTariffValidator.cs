using FluentValidation;
using SDCWebApp.Models;

namespace SDCWebApp.Data.Validation
{
    public class VisitTariffValidator : AbstractValidator<VisitTariff>, ICustomValidator<VisitTariff>
    {
        public VisitTariffValidator()
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
