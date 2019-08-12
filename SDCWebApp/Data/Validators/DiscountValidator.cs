using FluentValidation;
using SDCWebApp.Models;

namespace SDCWebApp.Data.Validators
{
    public class DiscountValidator : AbstractValidator<Discount>, ICustomValidator<Discount>
    {
        public DiscountValidator()
        {
            RuleFor(d => d.GroupSizeForDiscount)
                 .Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty().When(d => d.Type == Discount.DiscountType.ForGroup)
                 .Null().When(d => d.Type != Discount.DiscountType.ForGroup);

            RuleFor(d => d.Description)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(d => d.DiscountValueInPercentage)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .InclusiveBetween(0, 100).WithMessage("{PropertyName} is in percentage so should be between 0 and 100.");
        }
    }
}
