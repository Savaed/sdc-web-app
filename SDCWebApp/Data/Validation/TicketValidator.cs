using FluentValidation;
using SDCWebApp.Data.Validation.Extensions;
using SDCWebApp.Models;
using System;

namespace SDCWebApp.Data.Validation
{
    public class TicketValidator : AbstractValidator<Ticket>, ICustomValidator<Ticket>
    {
        public TicketValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.PurchaseDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(new DateTime(2012, 6, 2), DateTime.Now);  // 2.06.2012 is a date of company established.

            // Property ValidFor specify sightseeing group date for which was ordered.            
            RuleFor(x => x.ValidFor)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty()
            .Custom((validFor, context) =>
            {
                if (validFor != (context.InstanceToValidate as Ticket).Group.SightseeingDate)
                {
                    context.AddFailure($"Date {nameof(Ticket.ValidFor)} is not a sightseeing date of group for which the ticket was ordered.");
                }
            });

            RuleFor(x => x.TicketUniqueId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .BeGuid();

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().When(x => x.Discount != null && x.Tariff != null);

            RuleFor(x => x.Customer)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SetValidator(new CustomerValidator());

            RuleFor(x => x.Discount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is required.")
                .SetValidator(new DiscountValidator());

            RuleFor(x => x.Group)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is required.")
                .SetValidator(new SightseeingGroupValidator(dbContext));

            RuleFor(x => x.Tariff)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{PropertyName} is required.")
                .SetValidator(new TicketTariffValidator());
        }
    }
}
