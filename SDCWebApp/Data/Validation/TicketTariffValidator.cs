using FluentValidation;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class TicketTariffValidator : AbstractValidator<TicketTariff>, ICustomValidator<TicketTariff>
    {
        public TicketTariffValidator()
        {
            RuleFor(x => x.DefaultPrice)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .GreaterThan(0.0f).WithMessage("{PropertyName} should be greater than 0.")
                .LessThanOrEqualTo(1000.0f).WithMessage("{PropertyName} should be less than 1000.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}
