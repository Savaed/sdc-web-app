using FluentValidation;
using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validators
{
    public class TicketValidator : AbstractValidator<Ticket>, ICustomValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.PurchaseDate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(new DateTime(2012, 6, 2), DateTime.Now);

            RuleFor(x => x.ValidFor)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .InclusiveBetween(DateTime.Now, DateTime.Now.AddYears(1));

            RuleFor(x => x.TicketUniqueId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .BeGuid();
        }
    }
}
