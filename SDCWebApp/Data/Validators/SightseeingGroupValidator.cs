using FluentValidation;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validators
{
    public class SightseeingGroupValidator: AbstractValidator<SightseeingGroup>, ICustomValidator<SightseeingGroup>
    {
        public SightseeingGroupValidator()
        {
            RuleFor(x => x.MaxGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .InclusiveBetween(0, 40);

            RuleFor(x => x.CurrentGroupSize)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .InclusiveBetween(0, 40);

            RuleFor(x => x.SightseeingDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(DateTime.Now, DateTime.Now.AddYears(1))
                .Must(sightseeingDate => sightseeingDate.Minute == 0 || sightseeingDate.Minute == 30).WithMessage("{PropertyName} hour should be full or half past.");
        }
    }
}
