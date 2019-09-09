using FluentValidation;
using SDCWebApp.Data.Validation.Extensions;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class TicketValidator : AbstractValidator<Ticket>, ICustomValidator<Ticket>
    {
        public TicketValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.PurchaseDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(new DateTime(2012, 6, 2), DateTime.Now);  // 2.06.2012 is date of company established.

            RuleFor(x => x.ValidFor)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(DateTime.Now, DateTime.Now.AddDays(28));

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
