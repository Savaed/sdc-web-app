using FluentValidation;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Data.Validation
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>, ICustomValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Customer)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SetValidator(new CustomerDtoValidator());

            RuleForEach(x => x.Tickets)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .SetValidator(new TicketDtoValidator());
        }
    }
}
