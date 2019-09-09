using FluentValidation;
using SDCWebApp.Data.Validation.Extensions;
using SDCWebApp.Models;
using System;

namespace SDCWebApp.Data.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>, ICustomValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} required.")
                .GreaterThan(DateTime.Now.AddYears(-122).AddDays(-164)).WithMessage($"{nameof(Customer.DateOfBirth)} should be after {DateTime.Now.AddYears(-122).AddDays(-164).Date}")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} should be in the past.");

            // Info about email adresses acording https://en.wikipedia.org/wiki/Email_address and http://isemail.info
            RuleFor(x => x.EmailAddress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(132)
                .BeEmailAddress().WithMessage("Invalid email format");
        }
    }
}
