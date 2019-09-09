using FluentValidation;
using System;

using SDCWebApp.Data.Validation.Extensions;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Data.Validation
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>, ICustomValidator<TicketDto>
    {
        public TicketDtoValidator()
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
        }
    }
}
