using FluentValidation;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class VisitTariffDtoValidator : AbstractValidator<VisitTariffDto>, ICustomValidator<VisitTariffDto>
    {
        public VisitTariffDtoValidator()
        {
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);

            RuleForEach(x => x.TicketTariffs)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SetValidator(new TicketTariffDtoValidator());
        }
    }
}
