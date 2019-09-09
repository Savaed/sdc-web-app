using FluentValidation;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class SightseeingTariffDtoValidator : AbstractValidator<SightseeingTariffDto>, ICustomValidator<SightseeingTariffDto>
    {
        public SightseeingTariffDtoValidator()
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
