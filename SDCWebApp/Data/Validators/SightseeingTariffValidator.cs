using FluentValidation;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validators
{
    public class SightseeingTariffValidator : AbstractValidator<SightseeingTariff>, ICustomValidator<SightseeingTariff>
    {
        public SightseeingTariffValidator()
        {
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
